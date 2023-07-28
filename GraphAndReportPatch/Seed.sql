USE [ProjectNamePlaceHolder]
GO
INSERT [dbo].[Report] ([Id], [ReportName], [QueryType], [ReportOrChartType], [IsDistinct], [QueryString], [DisplayOnDashboard], [Sequence], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'544f5e44-9768-4c6b-bcb3-ebd1bf2dcc5d', N'Activity Logs - Horizontal Bar Graph', N'T-Sql', N'Horizontal Bar', 0, N'SELECT
      [Type] [Label]
     ,count(*) [Data]
  FROM [dbo].[AuditLogs]
  group by [Type]', 1, 0, N'DEFAULT', N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T11:24:39.2502216' AS DateTime2), N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T12:04:07.8123383' AS DateTime2))
INSERT [dbo].[Report] ([Id], [ReportName], [QueryType], [ReportOrChartType], [IsDistinct], [QueryString], [DisplayOnDashboard], [Sequence], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'709a680d-1b25-420e-97f3-ef9f23359047', N'Activity Logs - Pie Chart', N'T-Sql', N'Pie', 0, N'SELECT
      [Type] [Label]
     ,count(*) [Data]
  FROM [dbo].[AuditLogs]
  group by [Type]', 1, 3, N'DEFAULT', N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T11:30:49.6826542' AS DateTime2), N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T11:32:19.4277808' AS DateTime2))
INSERT [dbo].[Report] ([Id], [ReportName], [QueryType], [ReportOrChartType], [IsDistinct], [QueryString], [DisplayOnDashboard], [Sequence], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'b8dd761c-6f83-416f-9ea3-f169ab0e7acb', N'Activity Logs - Table', N'T-Sql', N'Table', 0, N'SELECT
      [Type] [Activity]
     ,count(*) [Count]
  FROM [dbo].[AuditLogs]
  group by [Type]', 1, 2, N'DEFAULT', N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T11:25:06.9683643' AS DateTime2), N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T11:32:16.6159796' AS DateTime2))
GO
INSERT [dbo].[ReportRoleAssignment] ([Id], [ReportId], [RoleName], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'0f0342c6-087b-4850-b45a-40e2c8942141', N'709a680d-1b25-420e-97f3-ef9f23359047', N'Admin', NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T11:32:19.4277790' AS DateTime2))
INSERT [dbo].[ReportRoleAssignment] ([Id], [ReportId], [RoleName], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'5f4e458a-0605-44a5-bf78-496118b8a613', N'544f5e44-9768-4c6b-bcb3-ebd1bf2dcc5d', N'Admin', NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T12:04:07.8121297' AS DateTime2))
INSERT [dbo].[ReportRoleAssignment] ([Id], [ReportId], [RoleName], [Entity], [CreatedBy], [CreatedDate], [LastModifiedBy], [LastModifiedDate]) VALUES (N'63539ac5-8471-4941-b208-f18f156433ca', N'b8dd761c-6f83-416f-9ea3-f169ab0e7acb', N'Admin', NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'd8638cbc-8c63-4eb7-b605-3b88db116bc1', CAST(N'2023-07-28T11:32:16.6159751' AS DateTime2))
GO
