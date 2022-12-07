using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.Batch.Queries;

public record GetBatchQuery : BaseQuery, IRequest<PagedListResponse<BatchState>>
{
    public string? CompanyId { get; set; }
}

public class GetBatchQueryHandler : BaseQueryHandler<ApplicationContext, BatchState, GetBatchQuery>, IRequestHandler<GetBatchQuery, PagedListResponse<BatchState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetBatchQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }
    public override async Task<PagedListResponse<BatchState>> Handle(GetBatchQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from company in Context.Batch
                     select company)
                            .AsNoTracking();
        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            query = query.Where(l => l.CompanyId == request.CompanyId);
        }
        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            query = from a in query
                    join c in Context.Company on a.CompanyId equals c.Id
                    join ue in Context.UserEntity on a.Id equals ue.CompanyId
                    where ue.PplusUserId == pplusUserId && c.IsDisabled == false
                    select a;
        }
        return await query.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    }
}
