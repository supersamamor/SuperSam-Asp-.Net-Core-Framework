
Create Table #TempCreditor (ID INT IDENTITY(1, 1) primary key , DBCode Varchar(30), EntityCode VarChar(30), CreditorAccount Varchar(255), AccountType Varchar(30), AccountNo Varchar(255), AuditUser Varchar(255), Email Varchar(100) null, EmailCC1 Varchar(100) null, EmailCC2 Varchar(100) null)														


Select a.Id, d.Id CreditorId,c.Id CompanyId, replace(a.AccountNo,'`','') AccountNo,
case when a.AccountType = 'CA' then 'Checking'
	 when a.AccountType = 'SA' then 'Savings' end AccountType,
a.Email,a.EmailCC1,a.EMailCC2,a.AuditUser
INTO #TempCreditorForUpdateInsert
from #TempCreditor as a
INNER JOIN dbo.DatabaseConnectionSetup as b on a.DbCode = b.Code
INNER JOIN dbo.Company as c on b.Id = c.DatabaseConnectionSetupId and a.EntityCode = c.Code
INNER JOIN dbo.Creditor as d on b.Id = d.DatabaseConnectionSetupId and a.CreditorAccount = d.CreditorAccount

Select Max(Id) Id,CompanyId,CreditorId,Count(*) [Count]
INTO #Duplicate
From #TempCreditorForUpdateInsert
Group by CreditorId,CompanyId
having Count(*) >1

--INSERT DUPLICATE
Declare @Entity NVARCHAR(100)		
Set @Entity = (Select Top 1 Id From [dbo].[Entities])

INSERT INTO EnrolledPayee ([Id]
      ,[CompanyId]
      ,[CreditorId]
      ,[PayeeAccountNumber]
      ,[PayeeAccountType]
      ,[Status]
      ,[Entity]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[LastModifiedBy]
      ,[LastModifiedDate]
      ,[Email]
      ,[EnrollmentBatchId])
Select  NewID(),a.CompanyId,a.CreditorId,AccountNo,AccountType,'Active'
,@Entity	
,AuditUser
,GetDate()
,AuditUser				
,GetDate()									 									
,Email		
,null
From #TempCreditorForUpdateInsert as a
Inner join #Duplicate as b on a.Id = b.Id

INSERT INTO EnrolledPayee ([Id]
      ,[CompanyId]
      ,[CreditorId]
      ,[PayeeAccountNumber]
      ,[PayeeAccountType]
      ,[Status]
      ,[Entity]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[LastModifiedBy]
      ,[LastModifiedDate]
      ,[Email]
      ,[EnrollmentBatchId])
Select  NewID(),a.CompanyId,a.CreditorId,AccountNo,AccountType,'Active'
,@Entity	
,AuditUser
,GetDate()
,AuditUser				
,GetDate()									 									
,Email		
,null
From #TempCreditorForUpdateInsert as a
Left join #Duplicate as b on a.CompanyId = b.CompanyId and a.CreditorId = b.CreditorId
Where b.Id is null

Insert Into EnrolledPayeeEmail (
[Id]
      ,[Email]
      ,[EnrolledPayeeId]
      ,[Entity]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[LastModifiedBy]
      ,[LastModifiedDate])
Select distinct NewID(),b.EmailCC1 ,a.Id EnrolledPayeeId,
@Entity
,AuditUser
,GetDate()
,AuditUser				
,GetDate()		
From [dbo].[EnrolledPayee] as a
Inner Join #TempCreditorForUpdateInsert as b on a.CompanyId = b.CompanyId and a.CreditorId = b.CreditorId
where b.EmailCC1 != ''

Insert Into EnrolledPayeeEmail (
[Id]
      ,[Email]
      ,[EnrolledPayeeId]
      ,[Entity]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[LastModifiedBy]
      ,[LastModifiedDate])
Select distinct NewID(),b.EmailCC2 ,a.Id EnrolledPayeeId,
@Entity
,AuditUser
,GetDate()
,AuditUser				
,GetDate()		
From [dbo].[EnrolledPayee] as a
Inner Join #TempCreditorForUpdateInsert as b on a.CompanyId = b.CompanyId and a.CreditorId = b.CreditorId
where b.EmailCC2 != ''