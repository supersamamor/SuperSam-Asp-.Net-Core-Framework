using CTI.Common.Utility.Extensions;
using CTI.Common.Utility.Models;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Core.Queries;
using CTI.DSF.Core.Identity;

namespace CTI.DSF.Web.Areas.Admin.Queries.Roles;

public record GetRolesQuery : BaseQuery, IRequest<PagedListResponse<ApplicationRole>>
{
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedListResponse<ApplicationRole>>
{
    private readonly IdentityContext _context;

    public GetRolesQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<ApplicationRole>> Handle(GetRolesQuery request, CancellationToken cancellationToken) =>
        await _context.Roles.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                            request.SortColumn, request.SortOrder,
                                                            request.PageNumber, request.PageSize, cancellationToken);
}
