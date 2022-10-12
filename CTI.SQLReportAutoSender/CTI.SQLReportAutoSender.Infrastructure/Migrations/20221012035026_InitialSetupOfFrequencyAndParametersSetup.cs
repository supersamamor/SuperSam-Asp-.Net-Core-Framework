using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTI.SQLReportAutoSender.Infrastructure.Migrations
{
    public partial class InitialSetupOfFrequencyAndParametersSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequency] ( [Id]
                                    ,[Description]
                                    ,[Entity]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[LastModifiedBy]
                                    ,[LastModifiedDate])
                    Values(NewId(),''" + Frequency.Monthly + @"'','''','''',GetDate(),'''',GetDate())')");

            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequency] ( [Id]
                                    ,[Description]
                                    ,[Entity]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[LastModifiedBy]
                                    ,[LastModifiedDate])
                    Values(NewId(),''" + Frequency.Weekly + @"'','''','''',GetDate(),'''',GetDate())')");

            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequency] ( [Id]
                                    ,[Description]
                                    ,[Entity]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[LastModifiedBy]
                                    ,[LastModifiedDate])
                    Values(NewId(),''" + Frequency.Daily + @"'','''','''',GetDate(),'''',GetDate())')");

            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequency] ( [Id]
                                    ,[Description]
                                    ,[Entity]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[LastModifiedBy]
                                    ,[LastModifiedDate])
                    Values(''" + Frequency.CustomDateId + @"'',''" + Frequency.CustomDates + @"'','''','''',GetDate(),'''',GetDate())')");

            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleParameter] ([Id]
                                    ,[Description]
                                    ,[Entity]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[LastModifiedBy]
                                    ,[LastModifiedDate])
                    Values(NewId(),''" + ScheduleParameter.Time + @"'','''','''',GetDate(),'''',GetDate())')");

            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleParameter] ([Id]
                                    ,[Description]
                                    ,[Entity]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[LastModifiedBy]
                                    ,[LastModifiedDate])
                    Values(NewId(),''" + ScheduleParameter.Dayname + @"'','''','''',GetDate(),'''',GetDate())')");

            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleParameter] ([Id]
                                    ,[Description]
                                    ,[Entity]
                                    ,[CreatedBy]
                                    ,[CreatedDate]
                                    ,[LastModifiedBy]
                                    ,[LastModifiedDate])
                    Values(NewId(),''" + ScheduleParameter.Daynumber + @"'','''','''',GetDate(),'''',GetDate())')");

            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequencyParameterSetup] ([Id]
                                                  ,[ScheduleFrequencyId]
                                                  ,[ScheduleParameterId]
                                                  ,[Entity]
                                                  ,[CreatedBy]
                                                  ,[CreatedDate]
                                                  ,[LastModifiedBy]
                                                  ,[LastModifiedDate])
                    Values(NewId(),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleFrequency] Where Description = ''" + Frequency.Monthly + @"''),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleParameter] Where Description = ''" + ScheduleParameter.Time + @"''),
                            '''',
                            '''',
                            GetDate(),
                            '''',
                            GetDate()
                            )')");
            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequencyParameterSetup] ([Id]
                                                  ,[ScheduleFrequencyId]
                                                  ,[ScheduleParameterId]
                                                  ,[Entity]
                                                  ,[CreatedBy]
                                                  ,[CreatedDate]
                                                  ,[LastModifiedBy]
                                                  ,[LastModifiedDate])
                    Values(NewId(),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleFrequency] Where Description = ''" + Frequency.Monthly + @"''),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleParameter] Where Description = ''" + ScheduleParameter.Daynumber + @"''),
                            '''',
                            '''',
                            GetDate(),
                            '''',
                            GetDate()
                            )')");
            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequencyParameterSetup] ([Id]
                                                  ,[ScheduleFrequencyId]
                                                  ,[ScheduleParameterId]
                                                  ,[Entity]
                                                  ,[CreatedBy]
                                                  ,[CreatedDate]
                                                  ,[LastModifiedBy]
                                                  ,[LastModifiedDate])
                    Values(NewId(),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleFrequency] Where Description = ''" + Frequency.Weekly + @"''),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleParameter] Where Description = ''" + ScheduleParameter.Time + @"''),
                            '''',
                            '''',
                            GetDate(),
                            '''',
                            GetDate()
                            )')");
            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequencyParameterSetup] ([Id]
                                                  ,[ScheduleFrequencyId]
                                                  ,[ScheduleParameterId]
                                                  ,[Entity]
                                                  ,[CreatedBy]
                                                  ,[CreatedDate]
                                                  ,[LastModifiedBy]
                                                  ,[LastModifiedDate])
                    Values(NewId(),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleFrequency] Where Description = ''" + Frequency.Weekly + @"''),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleParameter] Where Description = ''" + ScheduleParameter.Dayname + @"''),
                            '''',
                            '''',
                            GetDate(),
                            '''',
                            GetDate()
                            )')");
            migrationBuilder.Sql(@"Exec ('Insert Into [dbo].[ScheduleFrequencyParameterSetup] ([Id]
                                                  ,[ScheduleFrequencyId]
                                                  ,[ScheduleParameterId]
                                                  ,[Entity]
                                                  ,[CreatedBy]
                                                  ,[CreatedDate]
                                                  ,[LastModifiedBy]
                                                  ,[LastModifiedDate])
                    Values(NewId(),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleFrequency] Where Description = ''" + Frequency.Daily + @"''),
                            (SELECT TOP 1 [Id] FROM .[dbo].[ScheduleParameter] Where Description = ''" + ScheduleParameter.Time + @"''),
                            '''',
                            '''',
                            GetDate(),
                            '''',
                            GetDate()
                            )')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
