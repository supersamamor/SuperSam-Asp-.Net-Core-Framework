using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.Company.Queries;

public record GetCompanyQuery : BaseQuery, IRequest<PagedListResponse<CompanyState>>;

public class GetCompanyQueryHandler : BaseQueryHandler<ApplicationContext, CompanyState, GetCompanyQuery>, IRequestHandler<GetCompanyQuery, PagedListResponse<CompanyState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetCompanyQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }
    public override async Task<PagedListResponse<CompanyState>> Handle(GetCompanyQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from company in Context.Company
                     select company)
                            .AsNoTracking();

        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync();
            query = from a in query
                    join ue in Context.UserEntity on a.Id equals ue.CompanyId
                    where ue.PplusUserId == _authenticatedUser.UserId
                    select a;
        }
        return await query.Include(l => l.DatabaseConnectionSetup)
                    .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    }

}
