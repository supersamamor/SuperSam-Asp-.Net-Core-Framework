using CelerSoft.Common.Utility.Extensions;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CelerSoft.Common.Core.Queries;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Queries.Roles;

public record GetRolesQuery : BaseQuery, IRequest<PagedListResponse<IdentityRole>>
{
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedListResponse<IdentityRole>>
{
    private readonly IdentityContext _context;

    public GetRolesQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<IdentityRole>> Handle(GetRolesQuery request, CancellationToken cancellationToken) =>
        await _context.Roles.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                            request.SortColumn, request.SortOrder,
                                                            request.PageNumber, request.PageSize, cancellationToken);
}
