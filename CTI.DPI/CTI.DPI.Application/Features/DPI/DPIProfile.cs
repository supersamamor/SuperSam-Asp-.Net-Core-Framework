using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.DPI.Application.Features.DPI.Approval.Commands;
using CTI.DPI.Core.DPI;
using CTI.DPI.Application.Features.DPI.Report.Commands;
using CTI.DPI.Application.Features.DPI.ReportTable.Commands;
using CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;
using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;
using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;



namespace CTI.DPI.Application.Features.DPI;

public class DPIProfile : Profile
{
    public DPIProfile()
    {
        CreateMap<AddReportCommand, ReportState>();
		CreateMap <EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportTableCommand, ReportTableState>();
		CreateMap <EditReportTableCommand, ReportTableState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportTableJoinParameterCommand, ReportTableJoinParameterState>();
		CreateMap <EditReportTableJoinParameterCommand, ReportTableJoinParameterState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportColumnHeaderCommand, ReportColumnHeaderState>();
		CreateMap <EditReportColumnHeaderCommand, ReportColumnHeaderState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportColumnDetailCommand, ReportColumnDetailState>();
		CreateMap <EditReportColumnDetailCommand, ReportColumnDetailState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportFilterGroupingCommand, ReportFilterGroupingState>();
		CreateMap <EditReportFilterGroupingCommand, ReportFilterGroupingState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportColumnFilterCommand, ReportColumnFilterState>();
		CreateMap <EditReportColumnFilterCommand, ReportColumnFilterState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportQueryFilterCommand, ReportQueryFilterState>();
		CreateMap <EditReportQueryFilterCommand, ReportQueryFilterState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
