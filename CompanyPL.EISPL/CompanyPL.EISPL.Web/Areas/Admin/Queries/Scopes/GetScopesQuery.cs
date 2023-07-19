using CompanyPL.Common.Utility.Extensions;
using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Infrastructure.Data;
using CompanyPL.EISPL.Core.Oidc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyPL.Common.Core.Queries;

namespace CompanyPL.EISPL.Web.Areas.Admin.Queries.Scopes;

public record GetScopesQuery : BaseQuery, IRequest<PagedListResponse<OidcScope>>
{
}

public class GetScopesQueryHandler : IRequestHandler<GetScopesQuery, PagedListResponse<OidcScope>>
{
    private readonly IdentityContext _context;

    public GetScopesQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<OidcScope>> Handle(GetScopesQuery request, CancellationToken cancellationToken) =>
        await _context.Set<OidcScope>()
                      .AsNoTracking()
                      .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn,
                                       request.SortOrder, request.PageNumber, request.PageSize, cancellationToken);
}
