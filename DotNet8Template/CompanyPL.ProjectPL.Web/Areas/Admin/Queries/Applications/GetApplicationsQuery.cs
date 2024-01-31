using CompanyPL.Common.Utility.Extensions;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Infrastructure.Data;
using CompanyPL.ProjectPL.Core.Oidc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyPL.Common.Core.Queries;

namespace CompanyPL.ProjectPL.Web.Areas.Admin.Queries.Applications;

public record GetApplicationsQuery : BaseQuery, IRequest<PagedListResponse<OidcApplication>>
{
}

public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, PagedListResponse<OidcApplication>>
{
    private readonly IdentityContext _context;

    public GetApplicationsQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<OidcApplication>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken) =>
        await _context.Set<OidcApplication>()
                      .AsNoTracking()
                      .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn,
                                       request.SortOrder, request.PageNumber, request.PageSize, cancellationToken);
}
