using CNPlaceHolder.Common.Utility.Extensions;
using CNPlaceHolder.Common.Utility.Models;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CNPlaceHolder.Common.Core.Queries;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Queries.Roles;

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
