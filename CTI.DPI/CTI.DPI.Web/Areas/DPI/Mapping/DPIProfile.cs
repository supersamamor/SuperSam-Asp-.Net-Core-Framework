using AutoMapper;
using CTI.DPI.Core.DPI;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Application.Features.DPI.Approval.Commands;
using CTI.DPI.Application.Features.DPI.Report.Commands;
using CTI.DPI.Application.DTOs;

namespace CTI.DPI.Web.Areas.DPI.Mapping;

public class DPIProfile : Profile
{
    public DPIProfile()
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

        CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
        CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
        CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
        CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();


        CreateMap<ReportResultModel, ReportResultViewModel>().ReverseMap();
        CreateMap<ReportQueryFilterModel, ReportQueryFilterViewModel>().ReverseMap();
    }
}
