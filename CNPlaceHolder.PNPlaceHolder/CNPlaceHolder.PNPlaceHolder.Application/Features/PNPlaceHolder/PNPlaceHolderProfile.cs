using AutoMapper;
using CNPlaceHolder.Common.Core.Mapping;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.Approval.Commands;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;



namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder;

public class PNPlaceHolderProfile : Profile
{
    public PNPlaceHolderProfile()
    {
        CreateMap<AddModPlaceHolderCommand, ModPlaceHolderState>();
		CreateMap <EditModPlaceHolderCommand, ModPlaceHolderState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
