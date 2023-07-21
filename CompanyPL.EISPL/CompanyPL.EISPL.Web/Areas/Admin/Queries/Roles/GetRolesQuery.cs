using CompanyPL.Common.Utility.Extensions;
using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CompanyPL.Common.Core.Queries;
using CompanyPL.EISPL.Core.Identity;

namespace CompanyPL.EISPL.Web.Areas.Admin.Queries.Roles;

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
