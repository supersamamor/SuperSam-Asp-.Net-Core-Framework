using AutoMapper;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Application.Features.DSF.Report.Commands;
using CTI.DSF.Application.DTOs;
using CTI.DSF.Application.Features.DSF.Approval.Commands;
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


namespace CTI.DSF.Web.Areas.DSF.Mapping;

public class DSFProfile : Profile
{
    public DSFProfile()
    {
		CreateMap<ReportViewModel, AddReportCommand>()
            .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => new ReportRoleAssignmentState { RoleName = x })));
        CreateMap<ReportViewModel, EditReportCommand>()
           .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => new ReportRoleAssignmentState { RoleName = x })));
        CreateMap<ReportState, ReportViewModel>()
            .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => x.RoleName)));
        CreateMap<ReportViewModel, ReportState>();      
        CreateMap<ReportQueryFilterState, ReportQueryFilterViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportQueryFilterViewModel, ReportQueryFilterState>();
        CreateMap<ReportResultModel, ReportResultViewModel>().ReverseMap();
        CreateMap<ReportQueryFilterModel, ReportQueryFilterViewModel>().ReverseMap();
		
        CreateMap<CompanyViewModel, AddCompanyCommand>();
		CreateMap<CompanyViewModel, EditCompanyCommand>();
		CreateMap<CompanyState, CompanyViewModel>().ReverseMap();
		CreateMap<DepartmentViewModel, AddDepartmentCommand>();
		CreateMap<DepartmentViewModel, EditDepartmentCommand>();
		CreateMap<DepartmentState, DepartmentViewModel>().ForPath(e => e.ReferenceFieldCompanyCode, o => o.MapFrom(s => s.Company!.CompanyName));
		CreateMap<DepartmentViewModel, DepartmentState>();
		CreateMap<SectionViewModel, AddSectionCommand>();
		CreateMap<SectionViewModel, EditSectionCommand>();
		CreateMap<SectionState, SectionViewModel>().ForPath(e => e.ReferenceFieldDepartmentCode, o => o.MapFrom(s => s.Department!.Id));
		CreateMap<SectionViewModel, SectionState>();
		CreateMap<TeamViewModel, AddTeamCommand>();
		CreateMap<TeamViewModel, EditTeamCommand>();
		CreateMap<TeamState, TeamViewModel>().ForPath(e => e.ReferenceFieldSectionCode, o => o.MapFrom(s => s.Section!.Id));
		CreateMap<TeamViewModel, TeamState>();
		CreateMap<HolidayViewModel, AddHolidayCommand>();
		CreateMap<HolidayViewModel, EditHolidayCommand>();
		CreateMap<HolidayState, HolidayViewModel>().ReverseMap();
		CreateMap<TagsViewModel, AddTagsCommand>();
		CreateMap<TagsViewModel, EditTagsCommand>();
		CreateMap<TagsState, TagsViewModel>().ReverseMap();
		CreateMap<TaskMasterViewModel, AddTaskMasterCommand>();
		CreateMap<TaskMasterViewModel, EditTaskMasterCommand>();
		CreateMap<TaskMasterState, TaskMasterViewModel>().ReverseMap();
		CreateMap<TaskCompanyAssignmentViewModel, AddTaskCompanyAssignmentCommand>();
		CreateMap<TaskCompanyAssignmentViewModel, EditTaskCompanyAssignmentCommand>();
		CreateMap<TaskCompanyAssignmentState, TaskCompanyAssignmentViewModel>().ForPath(e => e.ReferenceFieldDepartmentId, o => o.MapFrom(s => s.Department!.Id)).ForPath(e => e.ReferenceFieldTaskMasterId, o => o.MapFrom(s => s.TaskMaster!.Id)).ForPath(e => e.ReferenceFieldCompanyId, o => o.MapFrom(s => s.Company!.CompanyName)).ForPath(e => e.ReferenceFieldSectionId, o => o.MapFrom(s => s.Section!.Id)).ForPath(e => e.ReferenceFieldTeamId, o => o.MapFrom(s => s.Team!.Id));
		CreateMap<TaskCompanyAssignmentViewModel, TaskCompanyAssignmentState>();
		CreateMap<TaskApproverViewModel, AddTaskApproverCommand>();
		CreateMap<TaskApproverViewModel, EditTaskApproverCommand>();
		CreateMap<TaskApproverState, TaskApproverViewModel>().ForPath(e => e.ReferenceFieldTaskCompanyAssignmentId, o => o.MapFrom(s => s.TaskCompanyAssignment!.Id));
		CreateMap<TaskApproverViewModel, TaskApproverState>();
		CreateMap<TaskTagViewModel, AddTaskTagCommand>();
		CreateMap<TaskTagViewModel, EditTaskTagCommand>();
		CreateMap<TaskTagState, TaskTagViewModel>().ForPath(e => e.ReferenceFieldTagId, o => o.MapFrom(s => s.Tags!.Name)).ForPath(e => e.ReferenceFieldTaskMasterId, o => o.MapFrom(s => s.TaskMaster!.Id));
		CreateMap<TaskTagViewModel, TaskTagState>();
		CreateMap<AssignmentViewModel, AddAssignmentCommand>();
		CreateMap<AssignmentViewModel, EditAssignmentCommand>();
		CreateMap<AssignmentState, AssignmentViewModel>().ForPath(e => e.ReferenceFieldTaskCompanyAssignmentId, o => o.MapFrom(s => s.TaskCompanyAssignment!.Id));
		CreateMap<AssignmentViewModel, AssignmentState>();
		CreateMap<DeliveryViewModel, AddDeliveryCommand>().ForPath(e => e.DeliveryAttachment, o => o.MapFrom(s => s.GeneratedDeliveryAttachmentPath));
		CreateMap<DeliveryViewModel, EditDeliveryCommand>().ForPath(e => e.DeliveryAttachment, o => o.MapFrom(s => s.GeneratedDeliveryAttachmentPath));
		CreateMap<DeliveryState, DeliveryViewModel>().ForPath(e => e.ReferenceFieldTaskCompanyAssignmentId, o => o.MapFrom(s => s.TaskCompanyAssignment!.Id)).ForPath(e => e.ReferenceFieldAssignmentId, o => o.MapFrom(s => s.Assignment!.Id));
		CreateMap<DeliveryViewModel, DeliveryState>();
		CreateMap<DeliveryApprovalHistoryViewModel, AddDeliveryApprovalHistoryCommand>();
		CreateMap<DeliveryApprovalHistoryViewModel, EditDeliveryApprovalHistoryCommand>();
		CreateMap<DeliveryApprovalHistoryState, DeliveryApprovalHistoryViewModel>().ForPath(e => e.ReferenceFieldDeliveryId, o => o.MapFrom(s => s.Delivery!.Id));
		CreateMap<DeliveryApprovalHistoryViewModel, DeliveryApprovalHistoryState>();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
