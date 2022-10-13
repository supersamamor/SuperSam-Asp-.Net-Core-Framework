using AutoMapper;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Approval.Commands;
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


namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Mapping;

public class SQLReportAutoSenderProfile : Profile
{
    public SQLReportAutoSenderProfile()
    {
        CreateMap<ScheduleFrequencyViewModel, AddScheduleFrequencyCommand>();
		CreateMap<ScheduleFrequencyViewModel, EditScheduleFrequencyCommand>();
		CreateMap<ScheduleFrequencyState, ScheduleFrequencyViewModel>().ReverseMap();
		CreateMap<ScheduleParameterViewModel, AddScheduleParameterCommand>();
		CreateMap<ScheduleParameterViewModel, EditScheduleParameterCommand>();
		CreateMap<ScheduleParameterState, ScheduleParameterViewModel>().ReverseMap();
		CreateMap<ScheduleFrequencyParameterSetupViewModel, AddScheduleFrequencyParameterSetupCommand>();
		CreateMap<ScheduleFrequencyParameterSetupViewModel, EditScheduleFrequencyParameterSetupCommand>();
		CreateMap<ScheduleFrequencyParameterSetupState, ScheduleFrequencyParameterSetupViewModel>().ForPath(e => e.ForeignKeyScheduleParameter, o => o.MapFrom(s => s.ScheduleParameter!.Description)).ForPath(e => e.ForeignKeyScheduleFrequency, o => o.MapFrom(s => s.ScheduleFrequency!.Description));
		CreateMap<ScheduleFrequencyParameterSetupViewModel, ScheduleFrequencyParameterSetupState>();
		
		CreateMap<ReportViewModel, AddReportCommand>();
		CreateMap<ReportViewModel, EditReportCommand>();
		CreateMap<ReportState, ReportViewModel>().ForPath(e => e.ForeignKeyScheduleFrequency, o => o.MapFrom(s => s.ScheduleFrequency!.Description));
		CreateMap<ReportViewModel, ReportState>();
		CreateMap<ReportDetailViewModel, AddReportDetailCommand>();
		CreateMap<ReportDetailViewModel, EditReportDetailCommand>();
		CreateMap<ReportDetailState, ReportDetailViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.Id));
		CreateMap<ReportDetailViewModel, ReportDetailState>();
		CreateMap<MailSettingViewModel, AddMailSettingCommand>();
		CreateMap<MailSettingViewModel, EditMailSettingCommand>();
		CreateMap<MailSettingState, MailSettingViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.Id));
		CreateMap<MailSettingViewModel, MailSettingState>();
		CreateMap<MailRecipientViewModel, AddMailRecipientCommand>();
		CreateMap<MailRecipientViewModel, EditMailRecipientCommand>();
		CreateMap<MailRecipientState, MailRecipientViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.Id));
		CreateMap<MailRecipientViewModel, MailRecipientState>();
		CreateMap<ReportScheduleSettingViewModel, AddReportScheduleSettingCommand>();
		CreateMap<ReportScheduleSettingViewModel, EditReportScheduleSettingCommand>();
		CreateMap<ReportScheduleSettingState, ReportScheduleSettingViewModel>().ForPath(e => e.ForeignKeyScheduleFrequency, o => o.MapFrom(s => s.ScheduleFrequency!.Description)).ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.Id)).ForPath(e => e.ForeignKeyScheduleParameter, o => o.MapFrom(s => s.ScheduleParameter!.Description));
		CreateMap<ReportScheduleSettingViewModel, ReportScheduleSettingState>();
		CreateMap<CustomScheduleViewModel, AddCustomScheduleCommand>();
		CreateMap<CustomScheduleViewModel, EditCustomScheduleCommand>();
		CreateMap<CustomScheduleState, CustomScheduleViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.Id));
		CreateMap<CustomScheduleViewModel, CustomScheduleState>();
		CreateMap<ReportInboxViewModel, AddReportInboxCommand>();
		CreateMap<ReportInboxViewModel, EditReportInboxCommand>();
		CreateMap<ReportInboxState, ReportInboxViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.Id));
		CreateMap<ReportInboxViewModel, ReportInboxState>();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
