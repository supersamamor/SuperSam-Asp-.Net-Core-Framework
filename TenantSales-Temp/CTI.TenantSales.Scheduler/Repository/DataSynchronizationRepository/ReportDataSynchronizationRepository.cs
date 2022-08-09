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
        public async Task RunReportDataSynchronizationScript(ProjectState project, int year = 0, int month = 0)
        {
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;
            try
            {
                string percentageRentTransactionType = "ARRB";
                string commonChargesFilter = "common";
                string airconChargesFilter = "aircon";
                string rentalChargesFilter = "rent";
                string invoiceClass = "I";
                string percentageSales = "P";
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


                        Delete From TenantLotMonthYear Where Year = '" + year + @"' and TenantID in (Select Id From Tenant Where ProjectId = '" + project.Id + @"')

						Insert Into TenantLotMonthYear(Id,[TenantID]
														,[Year]
														,[Month]
														,[LotNo]
														,[Area])
												SELECT Distinct newID(),ti.id TenantReportID,
                                                        d.fyear[Year],  
														d.fmonth Month,
                                                        prc.Lot_No LotNo,
                                                        prc.qty Area
                                                FROM #dates d
														INNER JOIN Tenant as ti on 1 = 1
                                                        INNER JOIN Project as p on ti.ProjectId = p.Id
                                                        INNER JOIN " + project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @".pm_rental_chrg prc ON d.end_date >= prc.start_date
                                                                                            AND d.end_date <= prc.end_date
                                                                                            AND prc.trx_type = '" + percentageRentTransactionType + @"'
                                                                                            AND prc.percentage_sales = '" + percentageSales + @"'
                                                                                            And p.Code = prc.Project_no
                                                                                            and ti.Code = prc.tenant_no
                                                Where d.fyear = '" + year + @"' and ti.ProjectId = '" + project.Id + @"'


                        Delete From TenantARDetailsMonthYear Where Year = '" + year + @"' and TenantID in (Select Id From Tenant Where ProjectId = '" + project.Id + @"')

						---INSERT CAMC
                        INSERT INTO TenantARDetailsMonthYear([Id],[TenantID]
                                , [Year]
                                , [Month]
                                , [CAMCRate]
                                , AirConRate
                                , SalesAmount
                                , MBaseAmount
                                , EffectiveRent)
                        SELECT NewID(), t.Id TenantReportID,
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
                                LEFT JOIN " + project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @".pm_std_chrg psc ON d.start_date >= psc.start_date
                                                                AND d.end_date <= psc.end_date
                                                                AND psc.descs LIKE '%" + commonChargesFilter + @"%'
                                                                And p.Code = psc.Project_no
                                                                and t.Code = psc.tenant_no
                        Where d.fyear = '" + year + @"' and t.ProjectId = '" + project.Id + @"'
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
                                 INNER JOIN " + project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @".pm_std_chrg psc ON d.start_date >= psc.start_date
                                                                   AND d.end_date <= psc.end_date
                                                                   AND psc.descs LIKE '%" + airconChargesFilter + @"%'
                                                                    And p.Code = psc.Project_no
                                                                    and t.Code = psc.tenant_no
                            Where d.fyear = '" + year + @"'
                            GROUP BY  t.Id,
                                   d.fyear,
                                   d.fmonth) as b
                        Where TenantId = TenantReportID2 and[Year] = b.Year2 and[Month] = b.Month2


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
                             INNER JOIN " + project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @".pos_tenant_sales pts ON d.fyear = pts.fyear
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
                             INNER JOIN " + project!.Company!.DatabaseConnectionSetup!.DatabaseAndServerName! + @".ar_ledger l ON d.fyear = l.fin_year
                                                           AND d.fmonth = l.fin_month and l.project_no = p.Code and l.debtor_acct = t.Code
                        Where d.fyear = '" + year + @"'
                        AND l.descs LIKE '%" + rentalChargesFilter + @"%'
                              AND l.class IN('" + invoiceClass + @"')
						GROUP BY  t.Id,
							   d.fyear, 
							   d.fmonth) as b
                        Where TenantId = TenantReportID2 and[Year] = b.Year2 and[Month] = b.Month2

						---CALCULATE Effective Rent
                        Update TenantARDetailsMonthYear
                        Set EffectiveRent = CASE WHEN ISNULL(b.Area, 0) = 0 THEN 0
												ELSE ISNULL(MBaseAmount, 0) / b.Area
                                            END
                        From(Select a.TenantID TenantReportId2
                                , a.[Year][Year2]
                                , a.[Month][Month2]
                                , Sum(IsNull(a.Area,0)) Area
                             From TenantLotMonthYear  as a
                             Where a.[Year] = '" + year + @"' 
						Group By a.TenantID,a.[Year],a.[Month]) as b
                        Where TenantID = TenantReportID2 and[Year] = b.Year2 and[Month] = b.Month2
                      Drop Table #Dates

					Declare @RenantTypeNonFixed Varchar(255)
					Set @RenantTypeNonFixed = (Select Top 1 Id From [dbo].[RentalType] Where Name = 'Non-Fixed')

							SELECT Distinct a.[Id]									 
									  ,a.[ProjectId]									 
									  ,a.[IsDisabled]		 
									  ,b.[TenantID]
									  ,b.[Year]
									  ,b.[Month]
									  ,b.[CAMCRate]
									  ,b.[AirConRate]
									  ,b.[SalesAmount]
									  ,b.[MBaseAmount]
									  ,b.[EffectiveRent]									
									  ,a.CategoryID
									  ,Sum(IsNull(c.Area,0)) Area
									  INTO #TenantARDetailsMonthYear_Raw
								  FROM [dbo].[Tenant] as a
								INNER JOIN [dbo].[TenantARDetailsMonthYear] as b on a.Id = b.TenantId
								LEFT JOIN [dbo].[TenantLotMonthYear] as c on b.TenantId = c.TenantId and b.Year = c.Year and b.Month = c.Month
								Where b.Year = " + year + @" and a.[RentalTypeId] = @RenantTypeNonFixed  And a.ProjectId = '" + project.Id + @"'
								Group By
								a.[Id]
									  ,a.[ProjectId]									
									  ,a.[IsDisabled]		
									  ,b.TenantId
									  ,b.[Year]
									  ,b.[Month]
									  ,b.[CAMCRate]
									  ,b.[AirConRate]
									  ,b.[SalesAmount]
									  ,b.[MBaseAmount]
									  ,b.[EffectiveRent]									
									  ,a.CategoryID

								Delete From ReportSalesGrowthPerformance Where [Year] = " + year + @" And TenantID in (Select Id From Tenant Where ProjectId = '" + project.Id + @"')

								INSERT INTO ReportSalesGrowthPerformance (
									  [Id]
									  ,[TenantId]									
									  ,[Year]
									  ,[Jan]
									  ,[Feb]
									  ,[Mar]
									  ,[Apr]
									  ,[May]
									  ,[Jun]
									  ,[Jul]
									  ,[Aug]
									  ,[Sep]
									  ,[Oct]
									  ,[Nov]
									  ,[Dec]
									  ,[JanCAMCAirConEffRent]
									  ,[FebCAMCAirConEffRent]
									  ,[MarCAMCAirConEffRent]
									  ,[AprCAMCAirConEffRent]
									  ,[MayCAMCAirConEffRent]
									  ,[JunCAMCAirConEffRent]
									  ,[JulCAMCAirConEffRent]
									  ,[AugCAMCAirConEffRent]
									  ,[SeptCAMCAirConEffRent]
									  ,[OctCAMCAirConEffRent]
									  ,[NovCAMCAirConEffRent]
									  ,[DecCAMCAirConEffRent])
								Select NewID(),a.[TenantId]									  
									  ," + year + @" [Year]
									  ,Sum(jan.[SalesAmount] / Case When jan.Area = 0 then 1 else jan.Area End) Jan
									  ,Sum(feb.[SalesAmount] / Case When feb.Area = 0 then 1 else feb.Area End) Feb
									  ,Sum(mar.[SalesAmount] / Case When mar.Area = 0 then 1 else mar.Area End) Mar
									  ,Sum(apr.[SalesAmount] / Case When apr.Area = 0 then 1 else apr.Area End) Apr
									  ,Sum(may.[SalesAmount] / Case When may.Area = 0 then 1 else may.Area End) May
									  ,Sum(jun.[SalesAmount] / Case When jun.Area = 0 then 1 else jun.Area End) Jun
									  ,Sum(jul.[SalesAmount] / Case When jul.Area = 0 then 1 else jul.Area End) Jul
									  ,Sum(aug.[SalesAmount] / Case When aug.Area = 0 then 1 else aug.Area End) Aug
									  ,Sum(sep.[SalesAmount] / Case When sep.Area = 0 then 1 else sep.Area End) Sep
									  ,Sum(oct.[SalesAmount] / Case When oct.Area = 0 then 1 else oct.Area End) Oct
									  ,Sum(nov.[SalesAmount] / Case When nov.Area = 0 then 1 else nov.Area End) Nov
									  ,Sum([dec].[SalesAmount] / Case When [dec].Area = 0 then 1 else [dec].Area End) [Dec]
									  ,Max(jan.CAMCRate) + Max(jan.AirConRate) + Max(jan.EffectiveRent) JanCAMCAirConEffRent	
									  ,Max(feb.CAMCRate) + Max(feb.AirConRate) + Max(feb.EffectiveRent) FebCAMCAirConEffRent
									  ,Max(mar.CAMCRate) + Max(mar.AirConRate) + Max(mar.EffectiveRent) MarCAMCAirConEffRent
									  ,Max(apr.CAMCRate) + Max(apr.AirConRate) + Max(apr.EffectiveRent) AprCAMCAirConEffRent
									  ,Max(may.CAMCRate) + Max(may.AirConRate) + Max(may.EffectiveRent) MayCAMCAirConEffRent
									  ,Max(jun.CAMCRate) + Max(jun.AirConRate) + Max(jun.EffectiveRent) JunCAMCAirConEffRent
									  ,Max(jul.CAMCRate) + Max(jul.AirConRate) + Max(jul.EffectiveRent) JulCAMCAirConEffRent
									  ,Max(aug.CAMCRate) + Max(aug.AirConRate) + Max(aug.EffectiveRent) AugCAMCAirConEffRent
									  ,Max(sep.CAMCRate) + Max(sep.AirConRate) + Max(sep.EffectiveRent) SeptCAMCAirConEffRent
									  ,Max(oct.CAMCRate) + Max(oct.AirConRate) + Max(oct.EffectiveRent) OctCAMCAirConEffRent
									  ,Max(nov.CAMCRate) + Max(nov.AirConRate) + Max(nov.EffectiveRent) NovCAMCAirConEffRent
									  ,Max([dec].CAMCRate) + Max([dec].AirConRate) + Max([dec].EffectiveRent) DecCAMCAirConEffRent		
								From (Select Distinct 
											   [TenantId]											 
											  ,[IsDisabled]			
											  ,[CategoryId]		
												From #TenantARDetailsMonthYear_Raw) as a
								LEFT JOIN #TenantARDetailsMonthYear_Raw as jan on a.TenantId = jan.TenantId and jan.Month = 1
								LEFT JOIN #TenantARDetailsMonthYear_Raw as feb on a.TenantId = feb.TenantId and feb.Month = 2
								LEFT JOIN #TenantARDetailsMonthYear_Raw as mar on a.TenantId = mar.TenantId and mar.Month = 3
								LEFT JOIN #TenantARDetailsMonthYear_Raw as apr on a.TenantId = apr.TenantId and apr.Month = 4
								LEFT JOIN #TenantARDetailsMonthYear_Raw as may on a.TenantId = may.TenantId and may.Month = 5
								LEFT JOIN #TenantARDetailsMonthYear_Raw as jun on a.TenantId = jun.TenantId and jun.Month = 6
								LEFT JOIN #TenantARDetailsMonthYear_Raw as jul on a.TenantId = jul.TenantId and jul.Month = 7
								LEFT JOIN #TenantARDetailsMonthYear_Raw as aug on a.TenantId = aug.TenantId and aug.Month = 8
								LEFT JOIN #TenantARDetailsMonthYear_Raw as sep on a.TenantId = sep.TenantId and sep.Month = 9
								LEFT JOIN #TenantARDetailsMonthYear_Raw as oct on a.TenantId = oct.TenantId and oct.Month = 10
								LEFT JOIN #TenantARDetailsMonthYear_Raw as nov on a.TenantId = nov.TenantId and nov.Month = 11
								LEFT JOIN #TenantARDetailsMonthYear_Raw as [dec] on a.TenantId = [dec].TenantId and [dec].Month = 12
								Group By a.[TenantId]									 
									  ,a.[IsDisabled]	
									  ,a.[CategoryId]	

								Drop Table #TenantARDetailsMonthYear_Raw
                            " + MonthlySalesGrowthReportScript(project.Id, year, month);
                _context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RunReportDataSynchronizationScript");
            }
        }
        private static string MonthlySalesGrowthReportScript(string projectId, int year, int month)
        {
            string script = @"
				Delete From [dbo].ReportSalesGrowthPerformanceMonth Where [Year] = " + year + @" And [Month] = " + month + @" And TenantID in (Select Id From Tenant Where ProjectId = '" + projectId + @"')

					---GET Minimum Month of the Year
					Select TenantId,[Year],Min([Month]) MinMonth
						Into #TenantLot
					From [dbo].TenantLotMonthYear Where [Year] = " + year + @"
					Group By  TenantId,[Year]

					Select RowId = Identity(int,1,1),a.[Year]," + month + @" [Month],Sum(c.Area) [Area],							
								 a.[Jan]
								,a.[Feb]
								,a.[Mar]
								,a.[Apr]
								,a.[May]
								,a.[Jun]
								,a.[Jul]
								,a.[Aug]
								,a.[Sep]
								,a.[Oct]
								,a.[Nov]
								,a.[Dec]
								,a.[JanCAMCAirConEffRent]
								,a.[FebCAMCAirConEffRent]
								,a.[MarCAMCAirConEffRent]
								,a.[AprCAMCAirConEffRent]
								,a.[MayCAMCAirConEffRent]
								,a.[JunCAMCAirConEffRent]
								,a.[JulCAMCAirConEffRent]
								,a.[AugCAMCAirConEffRent]
								,a.[SeptCAMCAirConEffRent]
								,a.[OctCAMCAirConEffRent]
								,a.[NovCAMCAirConEffRent]
								,a.[DecCAMCAirConEffRent]
								,d.Opening OpeningDate
								,d.DateTo EndDate								
							,Convert(Decimal(18,2),0) PrevYearYTD, Convert(Decimal(18,2),0) CurrentYearYTD, Convert(Decimal(18,2),0) PercentVariance, 
							Convert(Decimal(18,2),0) PercRentAirConCAMCOverCurrentYTDSales,Convert(Decimal(18,2),0) [PercRentAirConCAMCOverCurrentMonthSales]
							,a.TenantId			
							INTO #SalesGrowthMonthlyRaw
						From [dbo].[ReportSalesGrowthPerformance]  as a
						INNER JOIN #TenantLot as b on a.TenantId = b.TenantId and a.[Year] = b.[Year] 
						INNER JOIN [dbo].TenantLotMonthYear as c on a.TenantId = c.TenantId and a.[Year] = c.[Year] and b.MinMonth = c.[Month]
						INNER JOIN [dbo].Tenant as d on d.Id = a.TenantId
						Where a.[Year] = " + year + @"
						Group By a.[Year], a.[Jan]
								,a.[Feb]
								,a.[Mar]
								,a.[Apr]
								,a.[May]
								,a.[Jun]
								,a.[Jul]
								,a.[Aug]
								,a.[Sep]
								,a.[Oct]
								,a.[Nov]
								,a.[Dec]
							    ,a.TenantId	
								,a.[JanCAMCAirConEffRent]
								,a.[FebCAMCAirConEffRent]
								,a.[MarCAMCAirConEffRent]
								,a.[AprCAMCAirConEffRent]
								,a.[MayCAMCAirConEffRent]
								,a.[JunCAMCAirConEffRent]
								,a.[JulCAMCAirConEffRent]
								,a.[AugCAMCAirConEffRent]
								,a.[SeptCAMCAirConEffRent]
								,a.[OctCAMCAirConEffRent]
								,a.[NovCAMCAirConEffRent]
								,a.[DecCAMCAirConEffRent]
								,d.Opening
								,d.DateTo
							   

					Declare @PrevYearYTD Decimal(18,2)
					Declare @CurrentYearYTD Decimal(18,2)
					Declare @PercentVariance Decimal(18,2)
					Declare @PercRentAirConCAMCOverCurrentMonthSales Decimal(18,2)
					Declare @PercRentAirConCAMCOverCurrentYTDSales Decimal(18,2)

					Declare @Min Int
					Declare @Max Int
					Set @Min = 1
					Set @Max = (Select Count(*) From #SalesGrowthMonthlyRaw)
					While (@Min <= @Max)
					Begin
						Select Top 1 *
							INTO #CurrentSalesGrowth 		
						From #SalesGrowthMonthlyRaw Where RowID = @Min
			
						Select *
							INTO #PrevSalesGrowth 		
						From [dbo].[ReportSalesGrowthPerformance] Where TenantId in (Select Top 1 TenantId From #CurrentSalesGrowth) 
						And YEAR = (" + year + @" - 1)
		

						Set @PrevYearYTD = 0
						Set @CurrentYearYTD = 0
						Set @PercentVariance = 0
						Set @PercRentAirConCAMCOverCurrentMonthSales = 0
						Set @PercRentAirConCAMCOverCurrentYTDSales = 0

						--CALCULATE @PrevYearYTD
						Declare @PrevYearDivider Int = " + month + @"
						Declare @PrevYearSum Decimal(18,2) = 0
						If 1 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Jan From #PrevSalesGrowth) End
						If 2 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Feb From #PrevSalesGrowth) End
						If 3 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Mar From #PrevSalesGrowth) End
						If 4 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Apr From #PrevSalesGrowth) End
						If 5 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 May From #PrevSalesGrowth) End
						If 6 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Jun From #PrevSalesGrowth) End
						If 7 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Jul From #PrevSalesGrowth) End
						If 8 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Aug From #PrevSalesGrowth) End
						If 9 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Sep From #PrevSalesGrowth) End
						If 10 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Oct From #PrevSalesGrowth) End
						If 11 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Nov From #PrevSalesGrowth) End
						If 12 <= " + month + @" Begin Set @PrevYearSum = @PrevYearSum + (Select Top 1 Dec From #PrevSalesGrowth) End
						If (Select Top 1 Year(OpeningDate) From #CurrentSalesGrowth) = (" + year + @" - 1) And (Select Top 1 OpeningDate From #CurrentSalesGrowth) <= DateAdd(d,-1,DateAdd(m,1,Convert(DateTime,Convert(Varchar," + month + @") + '-01-' + Convert(Varchar,(" + year + @" - 1)))))
						Begin
							Set @PrevYearDivider = @PrevYearDivider - (Select Top 1 Month(OpeningDate) From #CurrentSalesGrowth) + 1			
						End
						If (Select Top 1 EndDate From #CurrentSalesGrowth) <= DateAdd(d,-1,DateAdd(m,1,Convert(DateTime,Convert(Varchar," + month + @") + '-01-' + Convert(Varchar,(" + year + @" - 1)))))		 
						Begin
							Set @PrevYearDivider = @PrevYearDivider - " + month + @" - (Select Top 1 Month(EndDate) From #CurrentSalesGrowth) + 1		
						End
						Set @PrevYearYTD = Case When @PrevYearDivider != 0 Then @PrevYearSum / @PrevYearDivider Else 0 End
						--CALCULATE @CurrentYearYTD
						Declare @CurrYearDivider Int = " + month + @"
						Declare @CurrYearSum Decimal(18,2) = 0
						If 1 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Jan From #CurrentSalesGrowth) End
						If 2 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Feb From #CurrentSalesGrowth) End
						If 3 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Mar From #CurrentSalesGrowth) End
						If 4 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Apr From #CurrentSalesGrowth) End
						If 5 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 May From #CurrentSalesGrowth) End
						If 6 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Jun From #CurrentSalesGrowth) End
						If 7 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Jul From #CurrentSalesGrowth) End
						If 8 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Aug From #CurrentSalesGrowth) End
						If 9 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Sep From #CurrentSalesGrowth) End
						If 10 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Oct From #CurrentSalesGrowth) End
						If 11 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Nov From #CurrentSalesGrowth) End
						If 12 <= " + month + @" Begin Set @CurrYearSum = @CurrYearSum + (Select Top 1 Dec From #CurrentSalesGrowth) End
						If (Select Top 1 Year(OpeningDate) From #CurrentSalesGrowth) = (" + year + @") And (Select Top 1 OpeningDate From #CurrentSalesGrowth) <= DateAdd(d,-1,DateAdd(m,1,Convert(DateTime,Convert(Varchar," + month + @") + '-01-' + Convert(Varchar,(" + year + @")))))
						Begin
							Set @CurrYearDivider = @CurrYearDivider - (Select Top 1 Month(OpeningDate) From #CurrentSalesGrowth) + 1			
						End
						If (Select Top 1 EndDate From #CurrentSalesGrowth) <= DateAdd(d,-1,DateAdd(m,1,Convert(DateTime,Convert(Varchar," + month + @") + '-01-' + Convert(Varchar,(" + year + @")))))		 
						Begin
							Set @CurrYearDivider = @CurrYearDivider - " + month + @" - (Select Top 1 Month(EndDate) From #CurrentSalesGrowth) + 1		
						End
						Set @CurrentYearYTD = Case When @CurrYearDivider != 0 Then @CurrYearSum / @CurrYearDivider Else 0 End
						--CALCULATE @PercentVariance
						Set @PercentVariance = Case When @PrevYearYTD = 0 Then 0 Else ((@CurrentYearYTD - @PrevYearYTD) / @PrevYearYTD) * 100 End 
						--CALCULATE @PercRentAirConCAMCOverCurrentMonthSales
						Declare @percRentAirConCAMCOver Decimal(18,2)

						If 1 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 JanCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 2 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 FebCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 3 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 MarCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 4 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 AprCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 5 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 MayCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 6 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 JunCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 7 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 JulCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 8 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 AugCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 9 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 SeptCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 10 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 OctCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 11 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 NovCAMCAirConEffRent From #CurrentSalesGrowth) End
						If 12 = " + month + @" Begin Set @percRentAirConCAMCOver = (Select Top 1 DecCAMCAirConEffRent From #CurrentSalesGrowth) End   
		 
						Declare @salesAmount Decimal(18,2)
						If 1 = " + month + @" Begin Set @salesAmount = (Select Top 1 Jan From #CurrentSalesGrowth) End
						If 2 = " + month + @" Begin Set @salesAmount = (Select Top 1 Feb From #CurrentSalesGrowth) End
						If 3 = " + month + @" Begin Set @salesAmount = (Select Top 1 Mar From #CurrentSalesGrowth) End
						If 4 = " + month + @" Begin Set @salesAmount = (Select Top 1 Apr From #CurrentSalesGrowth) End
						If 5 = " + month + @" Begin Set @salesAmount = (Select Top 1 May From #CurrentSalesGrowth) End
						If 6 = " + month + @" Begin Set @salesAmount = (Select Top 1 Jun From #CurrentSalesGrowth) End
						If 7 = " + month + @" Begin Set @salesAmount = (Select Top 1 Jul From #CurrentSalesGrowth) End
						If 8 = " + month + @" Begin Set @salesAmount = (Select Top 1 Aug From #CurrentSalesGrowth) End
						If 9 = " + month + @" Begin Set @salesAmount = (Select Top 1 Sep From #CurrentSalesGrowth) End
						If 10 = " + month + @" Begin Set @salesAmount = (Select Top 1 Oct From #CurrentSalesGrowth) End
						If 11 = " + month + @" Begin Set @salesAmount = (Select Top 1 Nov From #CurrentSalesGrowth) End
						If 12 = " + month + @" Begin Set @salesAmount = (Select Top 1 Dec From #CurrentSalesGrowth) End
						Set @PercRentAirConCAMCOverCurrentMonthSales = Case When @salesAmount = 0 Then 0 Else (@percRentAirConCAMCOver / @salesAmount) * 100 End 
						--CALCULATE @PercRentAirConCAMCOverCurrentYTDSales
						Set @PercRentAirConCAMCOverCurrentYTDSales = Case When @CurrentYearYTD = 0 Then 0 Else (@percRentAirConCAMCOver / @CurrentYearYTD) * 100 End 

						Update #SalesGrowthMonthlyRaw
						Set [PrevYearYTD] = IsNull(@PrevYearYTD,0),
							[CurrentYearYTD] = IsNull(@CurrentYearYTD,0), 
							[PercentVariance] = IsNull(@PercentVariance,0),
							[PercRentAirConCAMCOverCurrentMonthSales] = IsNull(@PercRentAirConCAMCOverCurrentMonthSales,0),
							[PercRentAirConCAMCOverCurrentYTDSales] = IsNull(@PercRentAirConCAMCOverCurrentYTDSales,0)	
						Where RowId = @Min

						Drop Table #CurrentSalesGrowth
						Drop Table #PrevSalesGrowth
						Set @Min = @Min + 1
					End

					INSERT INTO [dbo].[ReportSalesGrowthPerformanceMonth] ([Id],[Year]
					  ,[Month]				
					  ,[PrevYearYTD]
					  ,[CurrentYearYTD]
					  ,[PercentVariance]
					  ,[PercRentAirConCAMCOverCurrentMonthSales]
					  ,[PercRentAirConCAMCOverCurrentYTDSales]				
					  ,[Jan]
					  ,[Feb]
					  ,[Mar]
					  ,[Apr]
					  ,[May]
					  ,[Jun]
					  ,[Jul]
					  ,[Aug]
					  ,[Sep]
					  ,[Oct]
					  ,[Nov]
					  ,[Dec]
				      ,[TenantId])
					Select  NewID(),[Year]
					  ,[Month]				 
					  ,[PrevYearYTD]
					  ,[CurrentYearYTD]
					  ,[PercentVariance]
					  ,[PercRentAirConCAMCOverCurrentMonthSales]
					  ,[PercRentAirConCAMCOverCurrentYTDSales]					
					  ,[Jan]
					  ,[Feb]
					  ,[Mar]
					  ,[Apr]
					  ,[May]
					  ,[Jun]
					  ,[Jul]
					  ,[Aug]
					  ,[Sep]
					  ,[Oct]
					  ,[Nov]
					  ,[Dec]
				      ,[TenantId] From #SalesGrowthMonthlyRaw	

					Drop Table #SalesGrowthMonthlyRaw
					Drop Table #TenantLot

				";
            return script;
        }
    }
}
