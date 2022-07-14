using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Approval.Queries;

public record GetApproverSetupQuery : BaseQuery, IRequest<PagedListResponse<ApproverSetupState>>;

public class GetApproverSetupQueryHandler : BaseQueryHandler<ApplicationContext, ApproverSetupState, GetApproverSetupQuery>, IRequestHandler<GetApproverSetupQuery, PagedListResponse<ApproverSetupState>>
{
    public GetApproverSetupQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
