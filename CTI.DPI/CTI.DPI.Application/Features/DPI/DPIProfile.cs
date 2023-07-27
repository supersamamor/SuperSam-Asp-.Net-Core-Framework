using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.DPI.Application.Features.DPI.Approval.Commands;
using CTI.DPI.Core.DPI;
using CTI.DPI.Application.Features.DPI.Report.Commands;

namespace CTI.DPI.Application.Features.DPI;

public class DPIProfile : Profile
{
    public DPIProfile()
    {
        CreateMap<AddReportCommand, ReportState>();
		CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
