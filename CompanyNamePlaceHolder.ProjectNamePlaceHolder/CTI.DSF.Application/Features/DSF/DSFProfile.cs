using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.DSF.Application.Features.DSF.Approval.Commands;
using CTI.DSF.Core.DSF;
using CTI.DSF.Application.Features.DSF.Report.Commands;
using CTI.DSF.Application.Features.DSF.Company.Commands;
using CTI.DSF.Application.Features.DSF.Department.Commands;
using CTI.DSF.Application.Features.DSF.Section.Commands;
using CTI.DSF.Application.Features.DSF.Team.Commands;
using CTI.DSF.Application.Features.DSF.Holiday.Commands;
using CTI.DSF.Application.Features.DSF.Tags.Commands;
using CTI.DSF.Application.Features.DSF.TaskMaster.Commands;
using CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Commands;
using CTI.DSF.Application.Features.DSF.TaskApprover.Commands;
using CTI.DSF.Application.Features.DSF.TaskTag.Commands;
using CTI.DSF.Application.Features.DSF.Assignment.Commands;
using CTI.DSF.Application.Features.DSF.Delivery.Commands;
using CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Commands;



namespace CTI.DSF.Application.Features.DSF;

public class DSFProfile : Profile
{
    public DSFProfile()
    {
		CreateMap<AddReportCommand, ReportState>();
        CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		
        CreateMap<AddCompanyCommand, CompanyState>();
		CreateMap <EditCompanyCommand, CompanyState>().IgnoreBaseEntityProperties();
		CreateMap<AddDepartmentCommand, DepartmentState>();
		CreateMap <EditDepartmentCommand, DepartmentState>().IgnoreBaseEntityProperties();
		CreateMap<AddSectionCommand, SectionState>();
		CreateMap <EditSectionCommand, SectionState>().IgnoreBaseEntityProperties();
		CreateMap<AddTeamCommand, TeamState>();
		CreateMap <EditTeamCommand, TeamState>().IgnoreBaseEntityProperties();
		CreateMap<AddHolidayCommand, HolidayState>();
		CreateMap <EditHolidayCommand, HolidayState>().IgnoreBaseEntityProperties();
		CreateMap<AddTagsCommand, TagsState>();
		CreateMap <EditTagsCommand, TagsState>().IgnoreBaseEntityProperties();
		CreateMap<AddTaskMasterCommand, TaskMasterState>();
		CreateMap <EditTaskMasterCommand, TaskMasterState>().IgnoreBaseEntityProperties();
		CreateMap<AddTaskCompanyAssignmentCommand, TaskCompanyAssignmentState>();
		CreateMap <EditTaskCompanyAssignmentCommand, TaskCompanyAssignmentState>().IgnoreBaseEntityProperties();
		CreateMap<AddTaskApproverCommand, TaskApproverState>();
		CreateMap <EditTaskApproverCommand, TaskApproverState>().IgnoreBaseEntityProperties();
		CreateMap<AddTaskTagCommand, TaskTagState>();
		CreateMap <EditTaskTagCommand, TaskTagState>().IgnoreBaseEntityProperties();
		CreateMap<AddAssignmentCommand, AssignmentState>();
		CreateMap <EditAssignmentCommand, AssignmentState>().IgnoreBaseEntityProperties();
		CreateMap<AddDeliveryCommand, DeliveryState>();
		CreateMap <EditDeliveryCommand, DeliveryState>().IgnoreBaseEntityProperties();
		CreateMap<AddDeliveryApprovalHistoryCommand, DeliveryApprovalHistoryState>();
		CreateMap <EditDeliveryApprovalHistoryCommand, DeliveryApprovalHistoryState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
