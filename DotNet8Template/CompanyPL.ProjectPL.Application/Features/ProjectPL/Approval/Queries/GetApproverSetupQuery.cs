using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyPL.Common.Utility.Extensions;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Approval.Queries;

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
