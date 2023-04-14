using CelerSoft.Common.Utility.Extensions;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Infrastructure.Data;
using CelerSoft.TurboERP.Core.Oidc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CelerSoft.Common.Core.Queries;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Queries.Applications;

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
