using CTI.Common.Utility.Extensions;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Web.Areas.Identity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.Identity;

namespace CTI.TenantSales.Web.Areas.Admin.Queries.Users;

public record GetUsersQuery : BaseQuery, IRequest<PagedListResponse<ApplicationUser>>
{
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedListResponse<ApplicationUser>>
{
    private readonly IdentityContext _context;

    public GetUsersQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<ApplicationUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken) =>
        await _context.Users.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                            request.SortColumn, request.SortOrder,
                                                            request.PageNumber, request.PageSize, cancellationToken);
}
