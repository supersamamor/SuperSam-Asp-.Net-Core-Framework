using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Mapping;
Template:[InsertNewImportAppLayerMappingConfig]
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Commands;
Template:[InsertNewImportAreaPlaceHolderTextHere]


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder;

public class AreaPlaceHolderProfile : Profile
{
    public AreaPlaceHolderProfile()
    {
		CreateMap<AddReportCommand, ReportState>();
        CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		
        Template:[InsertNewApplicationLayerMapperConfigTextHerePropertyTextHere]
		Template:[ApprovalAppModelMapper]
    }
}
