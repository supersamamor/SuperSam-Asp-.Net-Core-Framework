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
    public class MasterfileSynchronizationRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MasterfileSynchronizationRepository> _logger;
        public MasterfileSynchronizationRepository(ApplicationContext context, ILogger<MasterfileSynchronizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task RunMasterFileSynchronizationScript(DatabaseConnectionSetupState databaseConnectionSetup)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
							Declare @FillCompanyQuery NVARCHAR(MAX)
							Declare @PplusDatabasePrefix NVARCHAR(100)
							Declare @Entity NVARCHAR(100)
							Set @PplusDatabasePrefix = (Select Top 1 [DatabaseAndServerName] From [dbo].[DatabaseConnectionSetup] Where Id = '" + databaseConnectionSetup.Id + @"')
							Set @Entity = (Select Top 1 Entity From [dbo].[DatabaseConnectionSetup] Where Id = '" + databaseConnectionSetup.Id + @"')

							Select b.[Id]
								  ,b.[CreatedDate]
								  ,b.[LastModifiedDate]
								  ,b.[Name]
								  ,b.[Code]
								  ,b.[EntityAddress]
								  ,b.[EntityAddressSecondLine]
								  ,b.[EntityDescription]
								  ,b.[EntityShortName]
								  ,b.[IsDisabled]
								  ,b.[TINNo]
								  ,b.[DatabaseConnectionSetupId] 
								  ,a.[entity_cd]
								  ,a.[entity_name]
								  ,a.[bs_div]
								  ,a.[bs_dept]
								  ,a.[base_currency]
								  ,isnull(a.address1,'') + ' ' + isnull(a.address2,'') + ' ' + isnull(a.address3,'')  [Address]   
								  ,a.[post_cd]
								  ,a.[telephone_no]
								  ,a.[fax_no]
								  ,a.[tax_descs]
								  ,a.[tax_reg_no]   
								  ,a.[budget_ctrl]
								  ,a.[budget_acct_dept]   
								  ,a.[curr_grp]
								  Into #EntityToUpdateAndInsert
								From " + databaseConnectionSetup.DatabaseAndServerName + @".cf_entity as a
								LEFT JOIN  [dbo].Company as b on a.Entity_cd = b.code and b.DatabaseConnectionSetupId = '" + databaseConnectionSetup.Id + @"'
								LEFT JOIN [dbo].[DatabaseConnectionSetup] as c on b.DatabaseConnectionSetupId = c.Id    
														
								Insert Into [dbo].[Company] (
								   [ID]
								  ,[CreatedDate]
								  ,[LastModifiedDate]
								  ,[Name]
								  ,[EntityAddress]     
								  ,[EntityDescription]    
								  ,[IsDisabled]
								  ,[TINNo]
								  ,[Code]
								  ,[DatabaseConnectionSetupId]
								  ,[Entity])
								Select Distinct 
								   NewID()
								  ,GetDate()
								  ,GetDate()
								  ,a.[entity_name]      
								  ,a.[Address]    
								  ,a.[entity_name]  
								  ,1
								  ,tax_reg_no
								  ,a.[entity_cd]
								  ,'" + databaseConnectionSetup.Id + @"'
								  ,@Entity
								From #EntityToUpdateAndInsert as a
								LEFT JOIN [dbo].[Company]  as b on b.DatabaseConnectionSetupId='" + databaseConnectionSetup.Id + @"' And a.[entity_cd] = b.Code
								Where b.Id is null

								Update [dbo].[Company] 
									Set [Name] = entity_name2, EntityAddress = [Address2],[TINNo] = tax_reg_no2
								From (Select ID Id2,[entity_name] entity_name2,[Address] [Address2],tax_reg_no tax_reg_no2 From #EntityToUpdateAndInsert) as a
								Where ID=ID2

								Drop Table #EntityToUpdateAndInsert


								SELECT   b.Id
									,a.[entity_cd]
									,a.[project_no]     
									,a.[descs] ProjectName
									,isnull(a.address1,'') + ' ' + isnull(a.address2,'')+ ' ' + isnull(a.address3,'') [Address]
									,c.Id CompanyiD  
									,c.[Name] CompanyName
									,c.Code CompanyCode								
									,d.Code DatabaseCode
									INTO #ProjectToUpdateAndInsert
							FROM " + databaseConnectionSetup.DatabaseAndServerName + @".[pl_project] as a
							LEFT JOIN [dbo].[Project] as b on a.[project_no] = b.Code
							LEFT JOIN [dbo].[Company] as c on c.Code = a.Entity_cd and c.DatabaseConnectionSetupId = '" + databaseConnectionSetup.Id + @"'
							LEFT JOIN [dbo].[DatabaseConnectionSetup] as d on c.DatabaseConnectionSetupId = d.Id 

							Insert Into [dbo].[Project] (
							   ID
							  ,[CompanyId]							
							  ,[Name]
							  ,[Code]
							  ,[ProjectAddress]     
							  ,[IsDisabled]
							  ,[CreatedBy]							
							  ,[LastModifiedBy]							 
							  ,[CreatedDate]
							  ,[LastModifiedDate]
							  ,[LandArea]
							  ,GLA
							  ,OutsideFC
							  ,HasAssociationDues
							  ,[Entity]
							  ,EnableMeterReadingApp)
							Select distinct 
									NewID()
									,a.[CompanyID]								
									,a.[ProjectName]
									,a.[project_no]
									,a.[Address]
									,1									
									,'System'
									,'System'								
									,GetDate()
									,GetDate()
									,0
									,0
									,0
									,0		
									,@Entity
									,0
							From #ProjectToUpdateAndInsert as a
							LEFT JOIN [dbo].[Project] as b on a.CompanyId = b.CompanyId And a.[project_no] = b.Code
							Where b.id is null	

							Drop Table #ProjectToUpdateAndInsert


							CREATE TABLE #ProjectFilter(
								[Id]  [varchar](255) NULL,
								[Code] [varchar](255) NULL,
								[IsDisabled] bit,
								[Name] [varchar](255) NULL,
							)
							Insert Into #ProjectFilter (Id,Code,[IsDisabled],[Name])
							Select a.Id,a.Code,a.[IsDisabled],a.[Name]
							From Project as a
							INNER JOIN dbo.Company as b on a.CompanyId = b.Id
							INNER JOIN dbo.DatabaseConnectionSetup as c on b.DatabaseConnectionSetupId = b.Id
							Where c.Id = '" + databaseConnectionSetup.Id + @"'

						CREATE TABLE #TenantToUpdateAndInsert(
								[TenantId]  [varchar](255) NULL,
								[Name] [varchar](60) NOT NULL,
								[Code] [varchar](20) NOT NULL,
								[FileCode] [int] NULL,
								[Folder] [int] NULL,
								[DateFrom] [datetime] NOT NULL,
								[DateTo] [datetime] NOT NULL,
								[Opening] [datetime] NOT NULL,
								[LevelDescs] [varchar](255) NULL,		
								[ProjectId] [varchar](255) NOT NULL,	
								[ProjectCode] [varchar](50) NULL,
								[ThemeName] [nvarchar](255) NULL,
								[Theme] [nvarchar](255) NULL,
								[ClassName] [nvarchar](255) NULL,
								[Class] [nvarchar](255) NULL,
								[CategoryName] [nvarchar](255) NULL,
								[Category] [nvarchar](255) NULL,
								[CategoryId] int NULL
							) 



						INSERT INTO  #TenantToUpdateAndInsert ([TenantId]
										  ,[Name]
										  ,[Code]
										  ,[FileCode]
										  ,[Folder]
										  ,[DateFrom]
										  ,[DateTo]
										  ,[Opening]
										  ,[LevelDescs]										
										  ,[ProjectId]
										  ,[ProjectCode]
										  ,[Theme]
										  ,[ThemeName]
										  ,[Class]
										  ,[ClassName]
										  ,[Category]
										  ,[CategoryName]	)
									SELECT Distinct i.Id
										,[trade_name] Name
										,a.[tenant_no] Code
										,null FileCode
										,null Folder
										,a.[commence_date] DateFrom
										,a.[expiry_date] DateTo
										,a.[commence_date] Opening
										,min(d.[level_descs]) LevelDescs
										,f.Id ProjectId  
										,f.Code ProjectCode
										,t.theme_cd
										,t.Descs Theme
										,cl.class_cd
										,cl.Descs Class
										,cat.category_cd
										,cat.Descs Category
									FROM " + databaseConnectionSetup.DatabaseAndServerName + @".[pm_tenancy] as a
									INNER JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".[pm_tenant_lot] as b on a.project_no = b.project_no and a.tenant_no = b.tenant_no
									INNER JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".[pm_lot] as c on a.project_no = c.project_no and b.lot_no = c.lot_no
									INNER JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".[pm_level] as d on a.project_no = d.project_no and c.level_cd = d.level_cd
									INNER JOIN (Select Distinct *
														From (SELECT project_no,tenant_no
																	FROM " + databaseConnectionSetup.DatabaseAndServerName + @".[pm_rental_chrg]
																	Union all
																SELECT project_no,tenant_no
																	FROM " + databaseConnectionSetup.DatabaseAndServerName + @".[pm_std_chrg]) as a) as h on a.project_no = h.project_no and a.tenant_no = h.tenant_no      
									INNER JOIN #ProjectFilter as f on a.[project_no] = f.Code	
									LEFT JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".pm_theme t ON a.theme_cd = t.theme_cd	
									LEFT JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".pm_class cl ON a.theme_cd = cl.theme_cd
																AND a.class_cd = cl.class_cd
									LEFT JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".pm_category cat ON a.theme_cd = cat.theme_cd
																AND a.class_cd = cat.class_cd
																AND a.category_cd = cat.category_cd									
									LEFT JOIN [dbo].[Tenant] as i on i.Code = a.tenant_no and f.Id = i.ProjectId 
									Where f.IsDisabled = 0
								Group By[trade_name]
								, a.[tenant_no]
								,a.[commence_date] 
								,a.[expiry_date] 
								,a.[commence_date]							 
								,f.Code     
								,f.Name  
								,i.Id  
								,f.Id 							
								,f.Code
								,t.theme_cd
								,t.Descs 
								,cl.class_cd
								,cl.Descs 
								,cat.category_cd
								,cat.Descs 

	
								
								Insert Into Theme ([Id]
									  ,[Name]
									  ,[Code]
									  ,[Entity]
									  ,[CreatedBy]
									  ,[CreatedDate]
									  ,[LastModifiedBy]
									  ,[LastModifiedDate])
								Select Distinct NewID(),a.ThemeName,a.Theme,@Entity,'System',GetDate(),'System',GetDate()
									From #TenantToUpdateAndInsert as a
									Left Join Theme as b on IsNull(a.Theme,'[No Theme]') = IsNull(b.Code,'[No Theme]')
									Where b.Id is null order By a.ThemeName Asc		



								Insert Into [dbo].[Classification] (Id, [Name],[Code] ,[Entity],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate],[ThemeId])
								Select Distinct NewID() Id,a.ClassName Name,a.Class Code,@Entity Entity,'System' CreatedBy,GetDate() CreatedDate,'System' LastModifiedBy,GetDate() LastModifiedDate,b.Id ThemeId   
									From #TenantToUpdateAndInsert as a
									INNER Join Theme as b on IsNull(a.Theme,'[No Theme]') = IsNull(b.Code,'[No Theme]')
									Left Join [Classification] as c on IsNull(a.Class,'[No Class]') = IsNull(c.Code,'[No Class]') and c.ThemeId = b.Id
									Where c.Id is null order By a.ClassName Asc
	

								Insert Into Category ([Id]
									  ,[Name]
									  ,[Code]
									  ,[Entity]
									  ,[CreatedBy]
									  ,[CreatedDate]
									  ,[LastModifiedBy]
									  ,[LastModifiedDate]
									  ,[ClassificationId])
								Select Distinct NewID(),a.CategoryName,a.Category,@Entity,'System',GetDate(),'System',GetDate(),c.Id ClassId 
									From #TenantToUpdateAndInsert as a
									INNER Join Theme as b on IsNull(a.Theme,'[No Theme]') = IsNull(b.Code,'[No Theme]')
									INNER Join Classification as c on IsNull(a.Class,'[No Class]') = IsNull(c.Code,'[No Class]') and c.ThemeId = b.Id
									LEFT Join Category as d on IsNull(a.Category,'[No Category]') = IsNull(d.Code,'[No Category]') and c.ThemeId = b.Id and d.ClassificationId = c.Id
									Where d.Id is null order By a.CategoryName Asc

								Update #TenantToUpdateAndInsert Set CategoryId = CategoryId2
								From (Select Distinct a.TenantId TenantId2, a.Category,a.CategoryName,c.Id ClassId,d.Id CategoryId2
									From #TenantToUpdateAndInsert as a
									INNER Join Theme as b on IsNull(a.Theme,'[No Theme]') = IsNull(b.Code,'[No Theme]')
									INNER Join Classification as c on IsNull(a.Class,'[No Class]') = IsNull(c.Code,'[No Class]') and c.ThemeId = b.Id
									INNER Join Category as d on IsNull(a.Category,'[No Category]') = IsNull(d.Code,'[No Category]') and c.ThemeId = b.Id and d.ClassificationId = c.Id) as a
									Where TenantId = TenantId2	
								
								---Set Theme, Class, Category to Default Values
								Update [dbo].[Theme]
										Set Code = '[No Theme]',Name = '[No Theme]'
										where Code Is Null

								Update [dbo].[Classification]
										Set Code = '[No Class]',Name = '[No Class]'
										where Code Is Null

								Update [dbo].[Category]
										Set Code = '[No Category]',Name = '[No Category]'
										where Code Is Null
								
								Insert Into [dbo].[Level] (
									 [Id]
									,[Name]								
									,[ProjectId]									
									,[IsDisabled]
									,[Entity]
									,[CreatedBy]
									,[CreatedDate]
									,[LastModifiedBy]
									,[LastModifiedDate]
									,[HasPercentageSalesTenant]
								)
								Select Distinct
								 NewID()
								 ,Upper(a.[LevelDescs])								
								 ,a.[ProjectId]
								 ,0
								 ,@Entity
								 ,'System'
								 ,GetDate()
								 ,'System'
								 ,GetDate()
								 ,0
								From #TenantToUpdateAndInsert as a
								INNER JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".[pm_level] as lv on a.ProjectCode = lv.project_no and Upper(a.LevelDescs) = Upper(lv.level_descs) 
								LEFT JOIN [dbo].[Level] as b on a.[ProjectId] = b.[ProjectId] And Upper(a.[LevelDescs]) = b.Name 
								Where b.Id is null

								Insert Into [dbo].[Tenant] (
									 Id
									,CreatedDate,LastModifiedDate,[Name]
									,[Code]    
									,[DateFrom]
									,[DateTo]
									,[Opening]
									,[LevelId]									
									,[IsDisabled]
									,[RentalTypeId]
									,[ProjectId]
									,[Entity])
								Select Distinct 
									NewId()
									,GetDate()
									,GetDate()
									,a.[Name]
									,a.[Code]    
									,a.[DateFrom]
									,a.[DateTo]
									,a.[Opening]
									,lv.Id								
									,0  
									,3
									,a.[ProjectId]	
									,@Entity
								From #TenantToUpdateAndInsert as a
								INNER JOIN dbo.[Level] as lv on a.ProjectId = lv.ProjectId and a.LevelDescs = lv.Name  
								LEFT JOIN [dbo].[Tenant] as b on a.[ProjectId] = b.[ProjectId] And a.[Code] = b.Code 
								Where b.Id is null
								
								Update [dbo].[Tenant] 
									Set CategoryId = CategoryId2
								From (Select CategoryId CategoryId2, [ProjectId] ProjectId2,[Code] TenantCode2 From #TenantToUpdateAndInsert) as a
								Where ProjectId = ProjectId2 and Code = TenantCode2	

								Insert Into [dbo].[SalesCategory] ([ID],[CreatedDate]
											  ,[LastModifiedDate]
											  ,[Code]
											  ,[Name]
											  ,[Rate]
											  ,[TenantId]
											  ,[IsDisabled]
											  ,[Entity])
								SELECT Distinct NewID(),GetDate(),
											GetDate(),			
											[sales_category] Code
											,[descs] Name
											,[tariff_percent] Rate
											,c.Id TenantId
											,0	
											,@Entity
										FROM " + databaseConnectionSetup.DatabaseAndServerName + @".[pos_tenant_tariff] as a
										INNER JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".[pos_sales_category] as b on a.sales_category = b.category_cd
										INNER JOIN #ProjectFilter as e on a.[project_no] = e.Code	
										INNER JOIN [dbo].[Tenant] as c on c.ProjectId = e.Id and c.Code = a.Tenant_No
										LEFT JOIN [dbo].[SalesCategory] as d on c.Id = d.TenantId and [sales_category] = d.Code
										Where d.Id is null

								---Update Name,DateFrom,DateTo,Opening
								Update [dbo].[Tenant] 
								Set [Name] = Name2,DateFrom = DateFrom2,DateTo = DateTo2,Opening = Opening2	
								From (Select TenantId Id2, [Name] Name2,DateFrom DateFrom2,DateTo DateTo2,Opening Opening2
										From #TenantToUpdateAndInsert as a) as a
								Where Id = Id2

								-----Update Tenant RentalTypeId
								Update [dbo].[Tenant]
								Set RentalTypeId = 2
								From (Select a.Id TenantId2,b.Code
										From [dbo].[Tenant] as a
										INNER JOIN [dbo].[SalesCategory] as b on a.Id = b.TenantId
										Where b.IsDisabled = 0) as a
								Where Id = TenantId2

								---Update Tenant Is Disabled/Enable All Non-Fixed regardless of Terms.
								Update [dbo].[Tenant] 
								Set IsDisabled = 1
								Where DateTo < GetDate() and RentalTypeId != 2

								---Update Level [HasPercentageSalesTenant]	
								Update [dbo].[Level] Set HasPercentageSalesTenant = 0

								Update [dbo].[Level] 
								Set HasPercentageSalesTenant = 1 
								From (Select Distinct c.Id Id2
										From [dbo].[Tenant] as a					
										INNER JOIN [dbo].[Level] as c on a.LevelId = c.ID
										Where a.IsDisabled = 0 and a.RentalTypeId = 2) as a
								Where id = id2
								
								Select e.Id ProjectId,e.Code ProjectCode, b.Code TenantCode,a.Lot_No,Max(a.Commence_date) Commence_date
										INTO  #TenantLotCommence
									From " + databaseConnectionSetup.DatabaseAndServerName + @".pm_tenant_Lot as a
									INNER JOIN #ProjectFilter as e on a.[project_no] = e.Code
									INNER JOIN [dbo].[Tenant] as b on a.Tenant_no = b.Code and b.ProjectId = e.Id
									Group By e.Id,e.Code,b.Code,a.Lot_No
									
								Update Tenant Set
									[Area] = [Area2]									 
								From (SELECT a.Id Id2,Sum(c.Area) Area2
									  FROM [dbo].[Tenant] as a
									INNER JOIN #TenantLotCommence as b on a.ProjectId = b.ProjectId and a.Code = b.TenantCode 
  									INNER JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".pm_tenant_Lot as c on a.Code = c.Tenant_no and b.ProjectCode = c.Project_no
																																	and b.Lot_No = c.Lot_No and b.Commence_date = c.Commence_Date
									Group By a.Id)  as a
								Where [Id] = [Id2]
								
								Drop Table #TenantLotCommence

								Drop Table #TenantToUpdateAndInsert    
								Drop Table #ProjectFilter

                            ";
                _context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RunMasterFileSynchronizationScript");
            }
        }
    }
}
