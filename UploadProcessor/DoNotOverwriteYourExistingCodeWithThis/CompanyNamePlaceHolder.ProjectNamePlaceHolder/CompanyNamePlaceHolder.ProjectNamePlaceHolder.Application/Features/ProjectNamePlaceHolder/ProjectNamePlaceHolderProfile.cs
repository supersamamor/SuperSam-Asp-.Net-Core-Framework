using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Mapping;

using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Report.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Commands;



namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder;

public class ProjectNamePlaceHolderProfile : Profile
{
    public ProjectNamePlaceHolderProfile()
    {
		CreateMap<AddReportCommand, ReportState>();
        CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		
        CreateMap<AddUploadProcessorCommand, UploadProcessorState>();
		CreateMap <EditUploadProcessorCommand, UploadProcessorState>().IgnoreBaseEntityProperties();
		CreateMap<AddUploadStagingCommand, UploadStagingState>();
		CreateMap <EditUploadStagingCommand, UploadStagingState>().IgnoreBaseEntityProperties();
		
		Template:[ApprovalAppModelMapper]
    }
}
