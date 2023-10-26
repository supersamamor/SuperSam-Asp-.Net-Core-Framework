using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UploadProcessorViewModel, AddUploadProcessorCommand>();
		CreateMap <UploadProcessorViewModel, EditUploadProcessorCommand>();
		CreateMap<UploadStagingViewModel, AddUploadStagingCommand>();
		CreateMap <UploadStagingViewModel, EditUploadStagingCommand>();
		
    }
}
