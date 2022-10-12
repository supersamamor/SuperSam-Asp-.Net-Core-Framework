using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Approval.Commands;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportDetail.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Commands;



namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender;

public class SQLReportAutoSenderProfile : Profile
{
    public SQLReportAutoSenderProfile()
    {
        CreateMap<AddScheduleFrequencyCommand, ScheduleFrequencyState>();
		CreateMap <EditScheduleFrequencyCommand, ScheduleFrequencyState>().IgnoreBaseEntityProperties();
		CreateMap<AddScheduleParameterCommand, ScheduleParameterState>();
		CreateMap <EditScheduleParameterCommand, ScheduleParameterState>().IgnoreBaseEntityProperties();
		CreateMap<AddScheduleFrequencyParameterSetupCommand, ScheduleFrequencyParameterSetupState>();
		CreateMap <EditScheduleFrequencyParameterSetupCommand, ScheduleFrequencyParameterSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportCommand, ReportState>();
		CreateMap <EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportDetailCommand, ReportDetailState>();
		CreateMap <EditReportDetailCommand, ReportDetailState>().IgnoreBaseEntityProperties();
		CreateMap<AddMailSettingCommand, MailSettingState>();
		CreateMap <EditMailSettingCommand, MailSettingState>().IgnoreBaseEntityProperties();
		CreateMap<AddMailRecipientCommand, MailRecipientState>();
		CreateMap <EditMailRecipientCommand, MailRecipientState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportScheduleSettingCommand, ReportScheduleSettingState>();
		CreateMap <EditReportScheduleSettingCommand, ReportScheduleSettingState>().IgnoreBaseEntityProperties();
		CreateMap<AddCustomScheduleCommand, CustomScheduleState>();
		CreateMap <EditCustomScheduleCommand, CustomScheduleState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportInboxCommand, ReportInboxState>();
		CreateMap <EditReportInboxCommand, ReportInboxState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
