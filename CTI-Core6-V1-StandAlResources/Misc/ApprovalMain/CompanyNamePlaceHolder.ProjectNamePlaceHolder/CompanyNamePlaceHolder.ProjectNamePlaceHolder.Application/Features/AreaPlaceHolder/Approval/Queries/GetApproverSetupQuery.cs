using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.Common.Utility.Extensions;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Approval.Queries;

public record GetApproverSetupQuery : BaseQuery, IRequest<PagedListResponse<ApproverSetupState>>;

public class GetApproverSetupQueryHandler : BaseQueryHandler<ApplicationContext, ApproverSetupState, GetApproverSetupQuery>, IRequestHandler<GetApproverSetupQuery, PagedListResponse<ApproverSetupState>>
{
    public GetApproverSetupQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<ApproverSetupState>> Handle(GetApproverSetupQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.ApproverSetup.Where(l=>l.ApprovalSetupType == ApprovalSetupTypes.Modular).AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                 request.SortColumn, request.SortOrder,
                                                                 request.PageNumber, request.PageSize,
                                                                 cancellationToken);
    }

}
