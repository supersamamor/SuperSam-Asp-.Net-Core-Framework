using AutoMapper;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.Approval.Commands;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;


namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Mapping;

public class PNPlaceHolderProfile : Profile
{
    public PNPlaceHolderProfile()
    {
        CreateMap<ModPlaceHolderViewModel, AddModPlaceHolderCommand>();
		CreateMap<ModPlaceHolderViewModel, EditModPlaceHolderCommand>();
		CreateMap<ModPlaceHolderState, ModPlaceHolderViewModel>().ReverseMap();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
