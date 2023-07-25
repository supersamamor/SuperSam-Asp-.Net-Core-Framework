using AutoMapper;
using CTI.DPI.API.Controllers.v1;
using CTI.DPI.Application.Features.DPI.Report.Commands;
using CTI.DPI.Application.Features.DPI.ReportTable.Commands;
using CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;
using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;
using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;


namespace CTI.DPI.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ReportViewModel, AddReportCommand>();
		CreateMap <ReportViewModel, EditReportCommand>();
		CreateMap<ReportTableViewModel, AddReportTableCommand>();
		CreateMap <ReportTableViewModel, EditReportTableCommand>();
		CreateMap<ReportTableJoinParameterViewModel, AddReportTableJoinParameterCommand>();
		CreateMap <ReportTableJoinParameterViewModel, EditReportTableJoinParameterCommand>();
		CreateMap<ReportColumnHeaderViewModel, AddReportColumnHeaderCommand>();
		CreateMap <ReportColumnHeaderViewModel, EditReportColumnHeaderCommand>();
		CreateMap<ReportColumnDetailViewModel, AddReportColumnDetailCommand>();
		CreateMap <ReportColumnDetailViewModel, EditReportColumnDetailCommand>();
		CreateMap<ReportFilterGroupingViewModel, AddReportFilterGroupingCommand>();
		CreateMap <ReportFilterGroupingViewModel, EditReportFilterGroupingCommand>();
		CreateMap<ReportColumnFilterViewModel, AddReportColumnFilterCommand>();
		CreateMap <ReportColumnFilterViewModel, EditReportColumnFilterCommand>();
		CreateMap<ReportQueryFilterViewModel, AddReportQueryFilterCommand>();
		CreateMap <ReportQueryFilterViewModel, EditReportQueryFilterCommand>();
		
    }
}
