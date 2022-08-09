using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTI.TenantSales.Scheduler.Repository.DataSynchronizationRepository
{
    public class ReportDataSynchronizationRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ReportDataSynchronizationRepository> _logger;
        public ReportDataSynchronizationRepository(ApplicationContext context, ILogger<ReportDataSynchronizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task RunReportDataSynchronizationScript(ProjectState project, int year = 0)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            try
            {
                string percentageRentTransactionType = "RBPT";
                string commonChargesFilter = "common";
                string airconChargesFilter = "aircon";
                string rentalChargesFilter = "rent";
                string invoiceClass = "I";
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
						CREATE TABLE #Dates (
							[fyear] [varchar](255) NULL,
							[fmonth] [int] NOT NULL,
							[start_date] [date] NULL,
							[end_date] [date] NULL
						) ON [PRIMARY]

						INSERT INTO #DATES ([fyear]
									  ,[fmonth]
									  ,[start_date]
									  ,[end_date])
							SELECT DISTINCT 
								  '" + year + @"' fyear, 
								  number fmonth, 
								  CONVERT(DATE, '" + year + @"'+'-'+CONVERT(VARCHAR, number)+'-1') start_date, 
								  DATEADD(d, -1, DATEADD(m, 1, CONVERT(DATE, '" + year + @"' + '-'+CONVERT(VARCHAR, number)+'-1'))) end_date
                                FROM master..spt_values
                                WHERE number BETWEEN 1 AND 12


                        Delete From TenantLotMonthYear Where Year = '" + year + @"' and TenantID in (Select Id From Tenant Where ProjectId = '"+ project.Id + @"')

						Insert Into TenantLotMonthYear([TenantReportID]
														,[Year]
														,[Month]
														,[LotNo]
														,[Area])
												SELECT Distinct ti.id TenantReportID,
                                                        d.fyear[Year],  
														d.fmonth Month,
                                                        prc.Lot_No LotNo,
                                                        prc.qty Area
                                                FROM #dates d
														INNER JOIN Tenant as ti on 1 = 1
                                                        INNER JOIN Project as p on ti.ProjectId = p.Id
                                                        INNER JOIN '"+ project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @"'.pm_rental_chrg prc ON d.end_date >= prc.start_date
                                                                                            AND d.end_date <= prc.end_date
                                                                                            AND prc.trx_type = '" + percentageRentTransactionType + @"'
                                                                                            AND prc.percentage_sales = 'P'
                                                                                            And p.Code = prc.Project_no
                                                                                            and ti.Code = prc.tenant_no
                                                Where d.fyear = '" + year + @"' and ti.ProjectId = '"+ project.Id + @"'


                        Delete From TenantARDetailsMonthYear Where Year = '" + year + @"' and TenantReportID in (Select Id From Tenant Where ProjectId = '"+ project.Id + @"')

						---INSERT CAMC
                        INSERT INTO TenantARDetailsMonthYear([TenantReportID]
                                , [Year]
                                , [Month]
                                , [CAMCRate]
                                , AirConRate
                                , SalesAmount
                                , MBaseAmount
                                , EffectiveRent)
                        SELECT t.Id TenantReportID,
                               d.fyear[Year], 
								d.fmonth[Month],  
								MAX(ISNULL(psc.rate, 0)) CAMCRate,
								0 AirConRate,
								0 SalesAmount,
								0 MBaseAmount,
								0 EffectiveRent
                        FROM #dates d      
								INNER JOIN Tenant as t on 1 = 1
                                INNER JOIN Project as p on t.ProjectId = p.Id
                                LEFT JOIN '"+ project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @"'.pm_std_chrg psc ON d.start_date >= psc.start_date
                                                                AND d.end_date <= psc.end_date
                                                                AND psc.descs LIKE '%"+ commonChargesFilter + @"%'
                                                                And p.Code = psc.Project_no
                                                                and t.Code = psc.tenant_no
                        Where d.fyear = '" + year + @"' and t.ProjectId = '"+ project.Id + @"'
                        GROUP BY  t.Id,
								d.fyear, 
								d.fmonth

                        -- - UPDATE AIRCON CAMC
                        Update TenantARDetailsMonthYear
                        Set AirConRate = AirConRate2
                        From(SELECT  t.Id TenantReportID2,
                                   d.fyear[Year2],
                                   d.fmonth[Month2],
                                   MAX(ISNULL(psc.rate, 0)) AirConRate2
                            FROM #dates d      
								 INNER JOIN Tenant as t on 1 = 1
                                 INNER JOIN Project as p on t.ProjectId = p.Id
                                 INNER JOIN '"+ project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @"'.pm_std_chrg psc ON d.start_date >= psc.start_date
                                                                   AND d.end_date <= psc.end_date
                                                                   AND psc.descs LIKE '%" + airconChargesFilter + @"%'
                                                                    And p.Code = psc.Project_no
                                                                    and t.Code = psc.tenant_no
                            Where d.fyear = '" + year + @"'
                            GROUP BY  t.Id,
                                   d.fyear,
                                   d.fmonth) as b
                        Where TenantReportID = TenantReportID2 and[Year] = b.Year2 and[Month] = b.Month2


                       -- - UPDATE Sales Amount
                        Update TenantARDetailsMonthYear
                        Set SalesAmount = SalesAmount2
                        From(SELECT  t.Id TenantReportID2,
                               d.fyear[Year2],
                               d.fmonth[Month2],
                               MAX(ISNULL(pts.total_amt, 0)) SalesAmount2
                        FROM #dates d      
							 INNER JOIN Tenant as t on 1 = 1
                             INNER JOIN Project as p on t.ProjectId = p.Id
                             INNER JOIN '"+ project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @"'.pos_tenant_sales pts ON d.fyear = pts.fyear
                                                                   AND d.fmonth = pts.fmonth and pts.project_no = p.Code and pts.debtor_acct = t.Code

                        Where d.fyear = '" + year + @"'
                        GROUP BY  t.Id,
                               d.fyear,
                               d.fmonth) as b
                        Where TenantID = TenantReportID2 and [Year] = b.Year2 and[Month] = b.Month2

                        --- UPDATE MBal Amount
                        Update TenantARDetailsMonthYear
                        Set MBaseAmount = mbase_amt2
                        From(SELECT  t.Id TenantReportID2,
                               d.fyear[Year2],
                               d.fmonth[Month2],
                               SUM(ISNULL(l.mbase_amt, 0)) mbase_amt2
                        FROM #dates d      
							 INNER JOIN Tenant as t on 1 = 1
                             INNER JOIN Project as p on t.ProjectId = p.Id
                             INNER JOIN '"+ project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @"'.ar_ledger l ON d.fyear = l.fin_year
                                                           AND d.fmonth = l.fin_month and l.project_no = p.Code and l.debtor_acct = t.Code
                        Where d.fyear = '" + year + @"'
                        AND l.descs LIKE '%" + rentalChargesFilter + @"%'
                              AND l.class IN('"+ invoiceClass + @"')
						GROUP BY  t.Id,
							   d.fyear, 
							   d.fmonth) as b
                        Where TenantReportID = TenantReportID2 and[Year] = b.Year2 and[Month] = b.Month2

						---CALCULATE Effective Rent
                        Update TenantARDetailsMonthYear
                        Set EffectiveRent = CASE WHEN ISNULL(b.Area, 0) = 0 THEN 0
												ELSE ISNULL(MBaseAmount, 0) / b.Area
                                            END
                        From(Select a.TenantReportId TenantReportId2
                                , a.[Year][Year2]
                                , a.[Month][Month2]
                                , Sum(IsNull(a.Area,0)) Area
                             From TenantLotMonthYear  as a
                             Where a.[Year] = '" + year + @"' 
						Group By a.TenantReportId,a.[Year],a.[Month]) as b
                        Where TenantReportID = TenantReportID2 and[Year] = b.Year2 and[Month] = b.Month2
                      Drop Table #Dates
                            ";
                _context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
    }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RunReportDataSynchronizationScript");
            }
        }
    }
}
