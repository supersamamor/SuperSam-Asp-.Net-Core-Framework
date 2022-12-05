using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.Creditor.Queries;

public record GetCreditorQuery : BaseQuery, IRequest<PagedListResponse<CreditorState>>
{
    public string? CompanyId { get; set; }
}

public class GetCreditorQueryHandler : BaseQueryHandler<ApplicationContext, CreditorState, GetCreditorQuery>, IRequestHandler<GetCreditorQuery, PagedListResponse<CreditorState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetCreditorQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }

    public override async Task<PagedListResponse<CreditorState>> Handle(GetCreditorQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from creditor in Context.Creditor
                     select creditor)
                            .AsNoTracking();

        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            query = from a in query
                    join c in Context.Company on a.DatabaseConnectionSetupId equals c.DatabaseConnectionSetupId
                    join ue in Context.UserEntity on c.Id equals ue.CompanyId
                    where ue.PplusUserId == _authenticatedUser.UserId && c.IsDisabled == false
                    select a;
        }
        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            var databaseConnectionSetupId = await Context.Company.Where(l => l.Id == request.CompanyId).AsNoTracking().Select(l => l.DatabaseConnectionSetupId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            query = query.Where(l => l.DatabaseConnectionSetupId == databaseConnectionSetupId);
        }
        return await query.Include(l => l.DatabaseConnectionSetup)
                    .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    }
}
