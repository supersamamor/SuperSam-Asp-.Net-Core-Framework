using AutoMapper;
using CTI.DPI.API.Controllers.v1;
using CTI.DPI.Application.Features.DPI.Report.Commands;

namespace CTI.DPI.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ReportViewModel, AddReportCommand>();
		CreateMap<ReportViewModel, EditReportCommand>();
    }
}
