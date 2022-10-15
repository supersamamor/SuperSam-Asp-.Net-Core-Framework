/****** Script for SelectTopNRows command from SSMS  ******/
Use [SQLReportAutoSender]

  --INSERT INTO [dbo].[Report]
  --(
	 --  [Id]
  --    ,[Description]
  --    ,[ScheduleFrequencyId]
  --    ,[IsActive]
  --    ,[LatestFileGeneratedPath]
  --    ,[MultipleReportType]
  --    ,[Entity]
  --    ,[CreatedBy]
  --    ,[CreatedDate]
  --    ,[LastModifiedBy]
  --    ,[LastModifiedDate]
  --)
  --SELECT a.[report_id]
  --    ,a.[report_descs]     
  --    ,case when a.[sched_id] = 1 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Monthly')
		--	when a.[sched_id] = 2 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Weekly') 
		--	when a.[sched_id] = 3 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Daily') 
		--	when a.[sched_id] = 4 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Custom Dates')
		--	else  (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Custom Dates')
		--	End Schedule
  --    ,a.[active_status]
  --    ,''
	 --  ,case when a.[multiple_report_type] = 0 then 1
		--	 when a.[multiple_report_type] = 1 then 2
		--	 when a.[multiple_report_type] = 2 then 1
		--	 End 
  --    ,'Default'
	 -- ,'System'
	 -- ,GetDate()
	 -- ,'System'
	 -- ,GetDate()    
  --FROM [Test].[dbo].[AutoSender_Reports] as a
  --LEFT JOIN [dbo].[Report] as b on a.report_id = b.id
  --where b.Id is null



-- Insert Into [dbo].[ReportDetail]
-- ( 
--       Id
--       ,[ReportId]
--      ,[ReportDetailNumber]
--      ,[Description]
--      ,[ConnectionString]
--      ,[QueryString]
--      ,[Entity]
--      ,[CreatedBy]
--      ,[CreatedDate]
--      ,[LastModifiedBy]
--      ,[LastModifiedDate]
--)
--  SELECT NewID()
--      ,a.[report_id]
--      ,a.[report_no]
--      ,a.[report_no_desc]
--      ,isnull(a.[conn_string] ,'')  
--	  ,isnull(a.[query_string],'')
--	  ,'Default'
--	  ,'System'
--	  ,GetDate()
--	  ,'System'
--	  ,GetDate()
--  FROM [Test].[dbo].[AutoSender_ReportsIndividual] as a
--  LEFT JOIN  [dbo].[ReportDetail] as b on a.report_id = b.ReportId and a.report_no = b.ReportDetailNumber
--  where b.Id is null



--INSERT INTO [dbo].[MailSetting]
--(
--[Id]
--      ,[ReportId]
--      ,[Account]
--      ,[Password]
--      ,[Body]
--      ,[Subject]
--      ,[Entity]
--      ,[CreatedBy]
--      ,[CreatedDate]
--      ,[LastModifiedBy]
--      ,[LastModifiedDate]
--)
--SELECT 
--NewID()
--,a.[report_id]
--      ,a.[mail_account]
--      ,a.[mail_password]
--      ,isnull(a.[mail_body],'')
--      ,isnull(a.[mail_subject],'')
--	  	  ,'Default'
--	  ,'System'
--	  ,GetDate()
--	  ,'System'
--	  ,GetDate()
--  FROM [Test].[dbo].[AutoSender_MailSettings] as a
--  INNER JOIN dbo.Report as c on a.report_id = c.Id
--  LEFT JOIN  [dbo].[MailSetting] as b on a.report_id = b.ReportId
--  where b.Id is null





--INSERT INTO [dbo].[CustomSchedule]
--(
-- [Id]
--      ,[ReportId]
--      ,[SequenceNumber]
--      ,[DateTimeSchedule]
--      ,[Entity]
--      ,[CreatedBy]
--      ,[CreatedDate]
--      ,[LastModifiedBy]
--      ,[LastModifiedDate]
--)

--SELECT 
--NewID()
--,a.[report_id]
--  ,a.[sequence_no]   
--      ,a.[datetime_sched]    
--	  	  	  ,'Default'
--	  ,'System'
--	  ,GetDate()
--	  ,'System'
--	  ,GetDate()
--  FROM [Test].[dbo].[AutoSender_CustomSequence] as a
--    INNER JOIN dbo.Report as c on a.report_id = c.Id
--	LEFT JOIN dbo.CustomSchedule as b on a.report_id = b.ReportId
--	where b.Id is null


--Insert Into  [dbo].[ReportScheduleSetting]
--(
-- [Id]
--      ,[ReportId]
--      ,[ScheduleFrequencyId]
--      ,[ScheduleParameterId]
--      ,[Value]
--      ,[Entity]
--      ,[CreatedBy]
--      ,[CreatedDate]
--      ,[LastModifiedBy]
--      ,[LastModifiedDate]
--  )
--SELECT NewId()
--      ,[report_id]   
--      ,case when a.[sched_id] = 1 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Monthly')
--			when a.[sched_id] = 2 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Weekly') 
--			when a.[sched_id] = 3 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Daily') 
--			when a.[sched_id] = 4 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Custom Dates')
--			else  (SELECT TOP 1 [Id] FROM [dbo].[ScheduleFrequency] Where Description = 'Custom Dates')
--			End Schedule
--      ,case when a.[sched_param_id] = 1 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleParameter] Where Description = 'Time')
--			when a.[sched_param_id] = 2 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleParameter] Where Description = 'Day name') 
--			when a.[sched_param_id] = 3 then (SELECT TOP 1 [Id] FROM [dbo].[ScheduleParameter] Where Description = 'Day number') 	
--			else  convert(varchar,a.[sched_param_id])
--			End Pram
--      ,[sched_value]
--	  ,'Default'
--	  ,'System'
--	  ,GetDate()
--	  ,'System'
--	  ,GetDate()
--  FROM [Test].[dbo].[AutoSender_SchedParamSettings] as a
--  INNER JOIN dbo.Report as c on a.report_id = c.Id
--  Left Join [dbo].[ReportScheduleSetting] as b on a.report_id=b.ReportId
--  where a.sched_value != '' and   a.sched_value is not null and   a.[sched_param_id] is not null
--  and b.Id is null