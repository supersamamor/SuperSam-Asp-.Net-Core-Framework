using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Mapping;

using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Commands;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder;

public class AreaPlaceHolderProfile : Profile
{
    public AreaPlaceHolderProfile()
    {
        CreateMap<AddReportCommand, ReportState>();
        CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();

        CreateMap<AddMainModulePlaceHolderCommand, MainModulePlaceHolderState>();
		CreateMap <EditMainModulePlaceHolderCommand, MainModulePlaceHolderState>().IgnoreBaseEntityProperties();
    }
}
