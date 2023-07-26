using AutoMapper;
using CTI.DPI.Core.DPI;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Application.Features.DPI.Approval.Commands;
using CTI.DPI.Application.Features.DPI.Report.Commands;
using CTI.DPI.Application.Features.DPI.ReportTable.Commands;
using CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;
using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;
using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;
using CTI.DPI.Application.DTOs;

namespace CTI.DPI.Web.Areas.DPI.Mapping;

public class DPIProfile : Profile
{
    public DPIProfile()
    {
        CreateMap<ReportViewModel, AddReportCommand>();
		CreateMap<ReportViewModel, EditReportCommand>();
		CreateMap<ReportState, ReportViewModel>().ReverseMap();
		CreateMap<ReportTableViewModel, AddReportTableCommand>();
		CreateMap<ReportTableViewModel, EditReportTableCommand>();
		CreateMap<ReportTableState, ReportTableViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
		CreateMap<ReportTableViewModel, ReportTableState>();
		CreateMap<ReportTableJoinParameterViewModel, AddReportTableJoinParameterCommand>();
		CreateMap<ReportTableJoinParameterViewModel, EditReportTableJoinParameterCommand>();
		CreateMap<ReportTableJoinParameterState, ReportTableJoinParameterViewModel>().ForPath(e => e.ForeignKeyReportTable, o => o.MapFrom(s => s.ReportTable!.Id)).ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
		CreateMap<ReportTableJoinParameterViewModel, ReportTableJoinParameterState>();
		CreateMap<ReportColumnHeaderViewModel, AddReportColumnHeaderCommand>();
		CreateMap<ReportColumnHeaderViewModel, EditReportColumnHeaderCommand>();
		CreateMap<ReportColumnHeaderState, ReportColumnHeaderViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
		CreateMap<ReportColumnHeaderViewModel, ReportColumnHeaderState>();
		CreateMap<ReportColumnDetailViewModel, AddReportColumnDetailCommand>();
		CreateMap<ReportColumnDetailViewModel, EditReportColumnDetailCommand>();
		CreateMap<ReportColumnDetailState, ReportColumnDetailViewModel>().ForPath(e => e.ForeignKeyReportTable, o => o.MapFrom(s => s.ReportTable!.Id)).ForPath(e => e.ForeignKeyReportColumnHeader, o => o.MapFrom(s => s.ReportColumnHeader!.Id));
		CreateMap<ReportColumnDetailViewModel, ReportColumnDetailState>();
		CreateMap<ReportFilterGroupingViewModel, AddReportFilterGroupingCommand>();
		CreateMap<ReportFilterGroupingViewModel, EditReportFilterGroupingCommand>();
		CreateMap<ReportFilterGroupingState, ReportFilterGroupingViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
		CreateMap<ReportFilterGroupingViewModel, ReportFilterGroupingState>();
		CreateMap<ReportColumnFilterViewModel, AddReportColumnFilterCommand>();
		CreateMap<ReportColumnFilterViewModel, EditReportColumnFilterCommand>();
		CreateMap<ReportColumnFilterState, ReportColumnFilterViewModel>().ForPath(e => e.ForeignKeyReportTable, o => o.MapFrom(s => s.ReportTable!.Id)).ForPath(e => e.ForeignKeyReportFilterGrouping, o => o.MapFrom(s => s.ReportFilterGrouping!.Id));
		CreateMap<ReportColumnFilterViewModel, ReportColumnFilterState>();
		CreateMap<ReportQueryFilterViewModel, AddReportQueryFilterCommand>();
		CreateMap<ReportQueryFilterViewModel, EditReportQueryFilterCommand>();
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
