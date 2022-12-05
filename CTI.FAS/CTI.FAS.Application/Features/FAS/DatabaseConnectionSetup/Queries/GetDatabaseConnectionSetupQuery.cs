using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.DatabaseConnectionSetup.Queries;

public record GetDatabaseConnectionSetupQuery : BaseQuery, IRequest<PagedListResponse<DatabaseConnectionSetupState>>;

public class GetDatabaseConnectionSetupQueryHandler : BaseQueryHandler<ApplicationContext, DatabaseConnectionSetupState, GetDatabaseConnectionSetupQuery>, IRequestHandler<GetDatabaseConnectionSetupQuery, PagedListResponse<DatabaseConnectionSetupState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetDatabaseConnectionSetupQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }
    public override async Task<PagedListResponse<DatabaseConnectionSetupState>> Handle(GetDatabaseConnectionSetupQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from database in Context.DatabaseConnectionSetup
                     select database)
                            .AsNoTracking();

        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            query = from a in query
                    join c in Context.Company on a.Id equals c.DatabaseConnectionSetupId
                    join ue in Context.UserEntity on c.Id equals ue.CompanyId
                    where ue.PplusUserId == pplusUserId && a.IsDisabled == false && c.IsDisabled == false
                    select a;
        }
        return await query
                    .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    }
}
