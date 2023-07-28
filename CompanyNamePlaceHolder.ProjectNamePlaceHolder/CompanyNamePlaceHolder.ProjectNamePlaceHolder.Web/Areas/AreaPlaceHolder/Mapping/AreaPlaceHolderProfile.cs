using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Mapping;

public class AreaPlaceHolderProfile : Profile
{
    public AreaPlaceHolderProfile()
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

        CreateMap<MainModulePlaceHolderViewModel, AddMainModulePlaceHolderCommand>();
		CreateMap<MainModulePlaceHolderViewModel, EditMainModulePlaceHolderCommand>();
		CreateMap<MainModulePlaceHolderState, MainModulePlaceHolderViewModel>().ReverseMap();	
    }
}
