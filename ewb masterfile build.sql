
USE FAS
--GO
--INSERT [dbo].[DatabaseConnectionSetup] ([Id], [Code], [Name], [DatabaseAndServerName], [InhouseDatabaseAndServerName], [SystemConnectionString], [IsDisabled], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'1', N'IFCA-FAIV2', N'FAI V2', N'[FAIALA-ID1.ad-fai.com].[FAI_PPlusLive_V2].[mgr]', N'[FAIALA-ID1.ad-fai.com].[SDG_PplusLive].[sdgadmin]', N'', 0, N'DEFAULT', N'System', CAST(N'2020-02-23T02:53:08.3100000' AS DateTime2), N'System', CAST(N'2020-02-23T02:53:08.3100000' AS DateTime2))
--INSERT [dbo].[DatabaseConnectionSetup] ([Id], [Code], [Name], [DatabaseAndServerName], [InhouseDatabaseAndServerName], [SystemConnectionString], [IsDisabled], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'2', N'IFCA-FAIV3', N'FAI V3', N'[FAIALA-ID1.ad-fai.com].[FAI_PPlusLive_V3].[mgr]', N'[FAIALA-ID1.ad-fai.com].[SDG_PplusLive_V3].[sdgadmin]', NULL, 1, N'DEFAULT', N'System', CAST(N'2020-02-23T02:53:08.3100000' AS DateTime2), N'89dcc2a6-8e8d-4d35-aa75-147d276766c7', CAST(N'2022-11-27T14:09:17.0326728' AS DateTime2))
--INSERT [dbo].[DatabaseConnectionSetup] ([Id], [Code], [Name], [DatabaseAndServerName], [InhouseDatabaseAndServerName], [SystemConnectionString], [IsDisabled], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'3', N'IFCA-FSIV2', N'FSI V2', N'[fsiala-id1.ad-fsi.com].[FSI_PPlusLive_V2].[mgr]', N'[fsiala-id1.ad-fsi.com].[FSI_SDG_PplusLive].[sdgadmin]', N'', 0, N'DEFAULT', N'System', CAST(N'2020-02-23T02:53:08.3100000' AS DateTime2), N'System', CAST(N'2020-02-23T02:53:08.3100000' AS DateTime2))
--INSERT [dbo].[DatabaseConnectionSetup] ([Id], [Code], [Name], [DatabaseAndServerName], [InhouseDatabaseAndServerName], [SystemConnectionString], [IsDisabled], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'4', N'IFCA-FAIV3_1', N'FAI V3.1', N'[FAIALA-ID1.ad-fai.com].[FAI_PPlusV3_1_Live].[mgr]', 1, N'', 0, N'DEFAULT', N'System', CAST(N'2021-11-24T10:20:26.0170000' AS DateTime2), N'System', CAST(N'2021-11-24T10:20:26.0170000' AS DateTime2))
--INSERT [dbo].[DatabaseConnectionSetup] ([Id], [Code], [Name], [DatabaseAndServerName], [InhouseDatabaseAndServerName], [SystemConnectionString], [IsDisabled], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'5', N'IFCA-FSIV3_1', N'FSI V3.1', N'[FSIALA-ID1.AD-FSI.COM].[FSI_PPlusV3_1_Live].[mgr]', 1, N'', 0, N'DEFAULT', N'System', CAST(N'2021-11-24T10:20:26.0470000' AS DateTime2), N'System', CAST(N'2021-11-24T10:20:26.0470000' AS DateTime2))
--GO


Declare @databaseConnectionSetupId Varchar  = 4

Declare @Entity NVARCHAR(100)						
							Set @Entity = (Select Top 1 Entity From [dbo].[DatabaseConnectionSetup] Where Id = @databaseConnectionSetupId)

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
								From [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].cf_entity as a
								LEFT JOIN  [dbo].Company as b on a.Entity_cd = b.code and b.DatabaseConnectionSetupId = @databaseConnectionSetupId
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
								  ,@databaseConnectionSetupId
								  ,@Entity
								  ,''
								  ,1
								  ,''
								  ,''
								  ,''
								  ,''								 
								From #EntityToUpdateAndInsert as a
								LEFT JOIN [dbo].[Company]  as b on b.DatabaseConnectionSetupId=@databaseConnectionSetupId And a.[entity_cd] = b.Code
								Where b.Id is null

								Update [dbo].[Company] 
									Set [Name] = entity_name2, EntityAddress = [Address2],[TINNo] = tax_reg_no2
								From (Select ID Id2,[entity_name] entity_name2,[Address] [Address2],tax_reg_no tax_reg_no2 From #EntityToUpdateAndInsert) as a
								Where ID=ID2

								DELETE FROM [UserEntity] Where CompanyId in (Select Id From [dbo].[Company] Where DatabaseConnectionSetupId = @databaseConnectionSetupId)

								Insert Into [dbo].[UserEntity] (
									   [Id]
									  ,[PplusUserId]
									  ,[CompanyId]									
									  ,[CreatedBy]
									  ,[LastModifiedBy]
									  ,[CreatedDate]									
									  ,[LastModifiedDate]
									  ,[Entity]
									)
								SELECT 
									   NewId()
									  ,a.[userid]
									  ,B.Id
									  ,'System'
									  ,'System'								
									  ,GetDate()
									  ,GetDate()									 									
									  ,@Entity	
								  FROM [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[cfs_user_entity] as a
								  LEFT JOIN [dbo].[Company]  as b on b.DatabaseConnectionSetupId=@databaseConnectionSetupId And a.[entity_cd] = b.Code
								  LEFT JOIN [dbo].[UserEntity] as c on b.Id = c.CompanyId And a.[userid] = c.[PplusUserId]
								  Where c.id is null	

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
							FROM [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[pl_project] as a
							LEFT JOIN [dbo].[Project] as b on a.[project_no] = b.Code
							LEFT JOIN [dbo].[Company] as c on c.Code = a.Entity_cd and c.DatabaseConnectionSetupId = @databaseConnectionSetupId
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
							Where c.Id = @databaseConnectionSetupId and a.IsDisabled = 0

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
									FROM [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[pm_tenancy] as a
									INNER JOIN [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[pm_tenant_lot] as b on a.project_no = b.project_no and a.tenant_no = b.tenant_no
									INNER JOIN [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[pm_lot] as c on a.project_no = c.project_no and b.lot_no = c.lot_no
									INNER JOIN [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[pm_level] as d on a.project_no = d.project_no and c.level_cd = d.level_cd
									INNER JOIN (Select Distinct *
														From (SELECT project_no,tenant_no
																	FROM [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[pm_rental_chrg]
																	Union all
																SELECT project_no,tenant_no
																	FROM [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[pm_std_chrg]) as a) as h on a.project_no = h.project_no and a.tenant_no = h.tenant_no      
									INNER JOIN #ProjectFilter as f on a.[project_no] = f.Code	
									LEFT JOIN [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].pm_theme t ON a.theme_cd = t.theme_cd	
									LEFT JOIN [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].pm_class cl ON a.theme_cd = cl.theme_cd
																AND a.class_cd = cl.class_cd
									LEFT JOIN [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].pm_category cat ON a.theme_cd = cat.theme_cd
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
								
				--INSERT CREDITOR
				INSERT INTO [dbo].[Creditor] ([Id]
						  ,[CreditorAccount]
						  ,[PayeeAccountName]
						  ,[PayeeAccountLongDescription]						
						  ,[PayeeAccountCode]
						  ,[PayeeAccountTIN]
						  ,[PayeeAccountAddress]
						  ,[Email]
						  ,[DatabaseConnectionSetupId]
						  ,[Entity]
						  ,[CreatedBy]
						  ,[CreatedDate]
						  ,[LastModifiedBy]
						  ,[LastModifiedDate])				
				  SELECT 
					newID()				
					,a.[creditor_acct]
					,a.[pay_to] [PayeeAccountName]
					,a.[pay_to] [LongDescription]	
					,isnull(a.[contra_acct],'') [PayeeAccountCode]	
					,isnull(b.Income_tax,'') [TinNumber]				
					,isnull(b.address1,'') + ' ' + isnull(b.address2,'') + ' ' + isnull(b.address3,'')  [Address]   
					,isnull(b.email,'') Email
					,@databaseConnectionSetupId
					,@Entity	
					,'System'
					,GetDate()
					,'System'	
					,GetDate()						
				  FROM [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[ap_creditor] as a
				LEFT JOIN  [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].[cf_business] as b on a.assoc_id = b.business_id
				LEFT JOIN [dbo].Creditor as c on a.[creditor_acct] = c.CreditorAccount and c.DatabaseConnectionSetupId = @databaseConnectionSetupId
				Where c.Id is null
	

				INSERT INTO [dbo].[PaymentTransaction] 
						([Id]
					  ,[EnrolledPayeeId]	
					  ,[DocumentNumber]
					  ,[DocumentDate]
					  ,[DocumentAmount]
					  ,[CheckNumber]					
					  ,[PaymentType]
					  ,[IfcaBatchNumber]
					  ,[IfcaLineNumber]	
					  ,[Status])			
				   SELECT NewID()
						,d.Id [EnrolledPayeeId]
						,a.doc_no
						,a.doc_date
						,a.trx_amt
						,''
						,case when e.descs like 'Check Prepare (EWBC)%' then 'Check Prepare' 
							  when e.descs like 'ESETTLE%' then 'E-Settle' end	[PaymentType]		  
						,a.[batch_no]
						,a.[line_no]
						,'New'					
                        FROM [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].ap_paytrx a       
						INNER JOIN [dbo].Company as b on a.entity_cd = b.Code and b.DatabaseConnectionSetupId = @databaseConnectionSetupId
						INNER JOIN [dbo].Creditor as c on a.[creditor_acct] = c.CreditorAccount and c.DatabaseConnectionSetupId = @databaseConnectionSetupId
						INNER JOIN [dbo].EnrolledPayee as d on b.Id = d.CompanyId and c.Id = d.CreditorId
						INNER JOIN [FAIALA-ID1.AD-FAI.COM].[FAI_PPlusV3_1_Live].[mgr].cf_trx_type as e on a.trx_type = e.trx_type and e.module = 'AP' and e.trx_class in ('M','A') and (e.descs like 'Check Prepare (EWBC)%' or e.descs like 'ESETTLE%')
                        LEFT JOIN [dbo].PaymentTransaction as f on d.Id = f.EnrolledPayeeId and a.batch_no = f.IfcaBatchNumber and a.line_no = f.IfcaLineNumber
						Where f.Id Is Null		

				Drop Table #TenantToUpdateAndInsert
				Drop Table #ProjectFilter


