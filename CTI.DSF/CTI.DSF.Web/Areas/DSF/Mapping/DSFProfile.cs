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
using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Application.Features.DSF.TaskApprover.Commands;
using CTI.DSF.Application.Features.DSF.TaskTag.Commands;
using CTI.DSF.Application.Features.DSF.Assignment.Commands;
using CTI.DSF.Application.Features.DSF.Delivery.Commands;


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
        CreateMap<ReportTableState, ReportTableViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportTableViewModel, ReportTableState>();
        CreateMap<ReportTableJoinParameterState, ReportTableJoinParameterViewModel>().ForPath(e => e.ForeignKeyReportTable, o => o.MapFrom(s => s.ReportTable!.Id)).ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportTableJoinParameterViewModel, ReportTableJoinParameterState>();
        CreateMap<ReportColumnHeaderState, ReportColumnHeaderViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportColumnHeaderViewModel, ReportColumnHeaderState>();
        CreateMap<ReportColumnDetailState, ReportColumnDetailViewModel>().ForPath(e => e.ForeignKeyReportTable, o => o.MapFrom(s => s.ReportTable!.Id)).ForPath(e => e.ForeignKeyReportColumnHeader, o => o.MapFrom(s => s.ReportColumnHeader!.Id));
        CreateMap<ReportColumnDetailViewModel, ReportColumnDetailState>();
        CreateMap<ReportFilterGroupingState, ReportFilterGroupingViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportFilterGroupingViewModel, ReportFilterGroupingState>();
        CreateMap<ReportColumnFilterState, ReportColumnFilterViewModel>().ForPath(e => e.ForeignKeyReportTable, o => o.MapFrom(s => s.ReportTable!.Id)).ForPath(e => e.ForeignKeyReportFilterGrouping, o => o.MapFrom(s => s.ReportFilterGrouping!.Id));
        CreateMap<ReportColumnFilterViewModel, ReportColumnFilterState>();
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
		CreateMap<TaskListViewModel, AddTaskListCommand>();
		CreateMap<TaskListViewModel, EditTaskListCommand>();
		CreateMap<TaskListState, TaskListViewModel>().ForPath(e => e.ReferenceFieldDepartmentId, o => o.MapFrom(s => s.Department!.Id)).ForPath(e => e.ReferenceFieldTeamId, o => o.MapFrom(s => s.Team!.Id)).ForPath(e => e.ReferenceFieldSectionId, o => o.MapFrom(s => s.Section!.Id)).ForPath(e => e.ReferenceFieldCompanyId, o => o.MapFrom(s => s.Company!.CompanyName));
		CreateMap<TaskListViewModel, TaskListState>();
		CreateMap<TaskApproverViewModel, AddTaskApproverCommand>();
		CreateMap<TaskApproverViewModel, EditTaskApproverCommand>();
		CreateMap<TaskApproverState, TaskApproverViewModel>().ForPath(e => e.ReferenceFieldTaskListId, o => o.MapFrom(s => s.TaskList!.Id));
		CreateMap<TaskApproverViewModel, TaskApproverState>();
		CreateMap<TaskTagViewModel, AddTaskTagCommand>();
		CreateMap<TaskTagViewModel, EditTaskTagCommand>();
		CreateMap<TaskTagState, TaskTagViewModel>().ForPath(e => e.ReferenceFieldTaskListId, o => o.MapFrom(s => s.TaskList!.Id)).ForPath(e => e.ReferenceFieldTagId, o => o.MapFrom(s => s.Tags!.Name));
		CreateMap<TaskTagViewModel, TaskTagState>();
		CreateMap<AssignmentViewModel, AddAssignmentCommand>();
		CreateMap<AssignmentViewModel, EditAssignmentCommand>();
		CreateMap<AssignmentState, AssignmentViewModel>().ForPath(e => e.ReferenceFieldTaskListId, o => o.MapFrom(s => s.TaskList!.Id));
		CreateMap<AssignmentViewModel, AssignmentState>();
		CreateMap<DeliveryViewModel, AddDeliveryCommand>().ForPath(e => e.DeliveryAttachment, o => o.MapFrom(s => s.GeneratedDeliveryAttachmentPath));
		CreateMap<DeliveryViewModel, EditDeliveryCommand>().ForPath(e => e.DeliveryAttachment, o => o.MapFrom(s => s.GeneratedDeliveryAttachmentPath));
		CreateMap<DeliveryState, DeliveryViewModel>().ForPath(e => e.ReferenceFieldAssignmentId, o => o.MapFrom(s => s.Assignment!.Id));
		CreateMap<DeliveryViewModel, DeliveryState>();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
