using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;
Template:[InsertNewImportWebLayerMappingConfig]
Template:[InsertNewImportAreaPlaceHolderTextHere]

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
        CreateMap<ReportQueryFilterState, ReportQueryFilterViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportQueryFilterViewModel, ReportQueryFilterState>();
        CreateMap<ReportResultModel, ReportResultViewModel>().ReverseMap();
        CreateMap<ReportQueryFilterModel, ReportQueryFilterViewModel>().ReverseMap();
		
        Template:[InsertNewCommandMapperConfigTextHere]
		Template:[ApprovalViewModelMapper]
    }
}
