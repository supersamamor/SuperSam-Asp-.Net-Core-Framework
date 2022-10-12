using AutoMapper;
using CTI.SQLReportAutoSender.API.Controllers.v1;
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


namespace CTI.SQLReportAutoSender.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ScheduleFrequencyViewModel, AddScheduleFrequencyCommand>();
		CreateMap <ScheduleFrequencyViewModel, EditScheduleFrequencyCommand>();
		CreateMap<ScheduleParameterViewModel, AddScheduleParameterCommand>();
		CreateMap <ScheduleParameterViewModel, EditScheduleParameterCommand>();
		CreateMap<ScheduleFrequencyParameterSetupViewModel, AddScheduleFrequencyParameterSetupCommand>();
		CreateMap <ScheduleFrequencyParameterSetupViewModel, EditScheduleFrequencyParameterSetupCommand>();
		CreateMap<ReportViewModel, AddReportCommand>();
		CreateMap <ReportViewModel, EditReportCommand>();
		CreateMap<ReportDetailViewModel, AddReportDetailCommand>();
		CreateMap <ReportDetailViewModel, EditReportDetailCommand>();
		CreateMap<MailSettingViewModel, AddMailSettingCommand>();
		CreateMap <MailSettingViewModel, EditMailSettingCommand>();
		CreateMap<MailRecipientViewModel, AddMailRecipientCommand>();
		CreateMap <MailRecipientViewModel, EditMailRecipientCommand>();
		CreateMap<ReportScheduleSettingViewModel, AddReportScheduleSettingCommand>();
		CreateMap <ReportScheduleSettingViewModel, EditReportScheduleSettingCommand>();
		CreateMap<CustomScheduleViewModel, AddCustomScheduleCommand>();
		CreateMap <CustomScheduleViewModel, EditCustomScheduleCommand>();
		CreateMap<ReportInboxViewModel, AddReportInboxCommand>();
		CreateMap <ReportInboxViewModel, EditReportInboxCommand>();
		
    }
}
