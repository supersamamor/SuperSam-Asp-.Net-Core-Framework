using CTI.Common.Utility.Extensions;
using CTI.Common.Utility.Models;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Core.Queries;
using CTI.FAS.Core.Identity;

namespace CTI.FAS.Web.Areas.Admin.Queries.Group;

public record GetGroupQuery : BaseQuery, IRequest<PagedListResponse<Core.Identity.Group>>
{
}

public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, PagedListResponse<Core.Identity.Group>>
{
    private readonly IdentityContext _context;

    public GetGroupQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<Core.Identity.Group>> Handle(GetGroupQuery request, CancellationToken cancellationToken) =>
        await _context.Group.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                               request.SortColumn, request.SortOrder,
                                                               request.PageNumber, request.PageSize,
                                                               cancellationToken);
}
