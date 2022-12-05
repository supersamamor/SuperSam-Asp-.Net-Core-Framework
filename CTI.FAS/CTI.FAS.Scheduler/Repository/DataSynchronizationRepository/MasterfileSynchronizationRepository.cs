using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CTI.FAS.Scheduler.Repository.DataSynchronizationRepository
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
							Declare @Entity NVARCHAR(100)						
							Set @Entity = (Select Top 1 Entity From [dbo].[DatabaseConnectionSetup] Where Id = '" + databaseConnectionSetup.Id + @"')

							Select Distinct b.[Id]
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
								  ,[Entity]
								  ,SubmitPlace
								  ,SubmitDeadline
								  ,EmailTelephoneNumber
								  ,BankName
								  ,BankCode
								  ,AccountNumber
								 )
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
								  ,''
								  ,1
								  ,''
								  ,''
								  ,''
								  ,''								 
								From #EntityToUpdateAndInsert as a
								LEFT JOIN [dbo].[Company]  as b on b.DatabaseConnectionSetupId='" + databaseConnectionSetup.Id + @"' And a.[entity_cd] = b.Code
								Where b.Id is null

								Update [dbo].[Company] 
									Set [Name] = entity_name2, EntityAddress = [Address2],[TINNo] = tax_reg_no2
								From (Select ID Id2,[entity_name] entity_name2,[Address] [Address2],tax_reg_no tax_reg_no2 From #EntityToUpdateAndInsert) as a
								Where ID=ID2								

								Drop Table #EntityToUpdateAndInsert

								SELECT  Distinct b.Id
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
							  ,[Entity]
							)
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
									,@Entity								
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
							INNER JOIN dbo.DatabaseConnectionSetup as c on b.DatabaseConnectionSetupId = c.Id
							Where c.Id = '" + databaseConnectionSetup.Id + @"' and a.IsDisabled = 0

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
								[CategoryId] [varchar](255) NULL
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


				Insert Into [dbo].[Tenant] (
						Id
					,CreatedDate,LastModifiedDate,[Name]
					,[Code]    
					,[DateFrom]
					,[DateTo]										
					,[IsDisabled]				
					,[ProjectId]
					,[Entity]
				)
				Select Distinct 
					NewId()
					,GetDate()
					,GetDate()
					,a.[Name]
					,a.[Code]    
					,a.[DateFrom]
					,a.[DateTo]								
					,0  				
					,a.[ProjectId]	
					,@Entity					
				From #TenantToUpdateAndInsert as a			
				LEFT JOIN [dbo].[Tenant] as b on a.[ProjectId] = b.[ProjectId] And a.[Code] = b.Code 
				Where b.Id is null	
	
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
