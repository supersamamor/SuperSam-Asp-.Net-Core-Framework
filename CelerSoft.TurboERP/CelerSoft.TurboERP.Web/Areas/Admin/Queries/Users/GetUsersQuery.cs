using CelerSoft.Common.Utility.Extensions;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.Identity;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Queries.Users;

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
