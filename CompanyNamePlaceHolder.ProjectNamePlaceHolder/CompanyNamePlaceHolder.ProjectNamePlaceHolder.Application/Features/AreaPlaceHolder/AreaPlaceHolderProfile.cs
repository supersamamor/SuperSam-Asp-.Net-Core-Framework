using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Mapping;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Approval.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.TaskList.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Assignment.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Commands;



namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder;

public class AreaPlaceHolderProfile : Profile
{
    public AreaPlaceHolderProfile()
    {
		CreateMap<AddReportCommand, ReportState>();
        CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		
        CreateMap<AddTaskListCommand, TaskListState>();
		CreateMap <EditTaskListCommand, TaskListState>().IgnoreBaseEntityProperties();
		CreateMap<AddAssignmentCommand, AssignmentState>();
		CreateMap <EditAssignmentCommand, AssignmentState>().IgnoreBaseEntityProperties();
		CreateMap<AddDeliveryCommand, DeliveryState>();
		CreateMap <EditDeliveryCommand, DeliveryState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
