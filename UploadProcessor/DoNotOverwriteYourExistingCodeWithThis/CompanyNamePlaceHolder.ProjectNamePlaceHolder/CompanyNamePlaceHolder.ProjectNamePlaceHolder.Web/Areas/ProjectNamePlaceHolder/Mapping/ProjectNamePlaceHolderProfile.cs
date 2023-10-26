using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Report.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;

using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Mapping;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
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
		
        CreateMap<UploadProcessorViewModel, AddUploadProcessorCommand>();
		CreateMap<UploadProcessorViewModel, EditUploadProcessorCommand>();
		CreateMap<UploadProcessorState, UploadProcessorViewModel>().ReverseMap();
		CreateMap<UploadStagingViewModel, AddUploadStagingCommand>();
		CreateMap<UploadStagingViewModel, EditUploadStagingCommand>();
		CreateMap<UploadStagingState, UploadStagingViewModel>().ForPath(e => e.ReferenceFieldUploadProcessorId, o => o.MapFrom(s => s.UploadProcessor!.Id));
		CreateMap<UploadStagingViewModel, UploadStagingState>();
		
		Template:[ApprovalViewModelMapper]
    }
}
