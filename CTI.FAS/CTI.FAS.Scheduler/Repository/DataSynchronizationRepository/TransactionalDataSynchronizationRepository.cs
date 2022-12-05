using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CTI.FAS.Scheduler.Repository.DataSynchronizationRepository
{
    public class TransactionalDataSynchronizationRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<TransactionalDataSynchronizationRepository> _logger;
        public TransactionalDataSynchronizationRepository(ApplicationContext context, ILogger<TransactionalDataSynchronizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task RunTransactionalDataSynchronizationScript(DatabaseConnectionSetupState databaseConnectionSetup)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandTimeout = 600;
                command.CommandType = CommandType.Text;
                command.CommandText = @"		
							Declare @Entity NVARCHAR(100)						
							Set @Entity = (Select Top 1 Entity From [dbo].[DatabaseConnectionSetup] Where Id = '" + databaseConnectionSetup.Id + @"')							

							DELETE FROM [UserEntity] Where CompanyId in (Select Id From [dbo].[Company] Where DatabaseConnectionSetupId = '" + databaseConnectionSetup.Id + @"')

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
								FROM " + databaseConnectionSetup.DatabaseAndServerName + @".[cfs_user_entity] as a
								LEFT JOIN [dbo].[Company]  as b on b.DatabaseConnectionSetupId='" + databaseConnectionSetup.Id + @"' And a.[entity_cd] = b.Code
								LEFT JOIN [dbo].[UserEntity] as c on b.Id = c.CompanyId And a.[userid] = c.[PplusUserId]
								Where c.id is null	
								
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
								,'" + databaseConnectionSetup.Id + @"'
								,@Entity	
								,'System'
								,GetDate()
								,'System'	
								,GetDate()						
							  FROM " + databaseConnectionSetup.DatabaseAndServerName + @".[ap_creditor] as a
							LEFT JOIN  " + databaseConnectionSetup.DatabaseAndServerName + @".[cf_business] as b on a.assoc_id = b.business_id
							LEFT JOIN [dbo].Creditor as c on a.[creditor_acct] = c.CreditorAccount and c.DatabaseConnectionSetupId = '" + databaseConnectionSetup.Id + @"'
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
									,case when e.descs like 'Check Prepare (EWBC)%' then '" + PaymentType.CheckPrepare + @"'
										  when e.descs like 'ESETTLE%' then '" + PaymentType.ESettle + @"' end	[PaymentType]		  
									,a.[batch_no]
									,a.[line_no]
									,'" + PaymentTransactionStatus.New + @"'					
									FROM " + databaseConnectionSetup.DatabaseAndServerName + @".ap_paytrx a       
									INNER JOIN [dbo].Company as b on a.entity_cd = b.Code and b.DatabaseConnectionSetupId = '" + databaseConnectionSetup.Id + @"'
									INNER JOIN [dbo].Creditor as c on a.[creditor_acct] = c.CreditorAccount and c.DatabaseConnectionSetupId = '" + databaseConnectionSetup.Id + @"'
									INNER JOIN [dbo].EnrolledPayee as d on b.Id = d.CompanyId and c.Id = d.CreditorId
									INNER JOIN " + databaseConnectionSetup.DatabaseAndServerName + @".cf_trx_type as e on a.trx_type = e.trx_type and e.module = '" + IFCAModule.AP + @"' and e.trx_class in (" + IFCAConstant.APTransactionClass + @") and (e.descs like '" + IFCAConstant.TransactionTypeDescriptionPrefix_CheckPrepare + @"%' or e.descs like '" + IFCAConstant.TransactionTypeDescriptionPrefix_ESettle + @"%')
									LEFT JOIN [dbo].PaymentTransaction as f on d.Id = f.EnrolledPayeeId and a.batch_no = f.IfcaBatchNumber and a.line_no = f.IfcaLineNumber
									Where f.Id Is Null	
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
