using CNPlaceHolder.Common.Utility.Extensions;
using CNPlaceHolder.Common.Utility.Models;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CNPlaceHolder.Common.Core.Queries;
using CNPlaceHolder.PNPlaceHolder.Core.Identity;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Queries.Entities;

public record GetEntitiesQuery : BaseQuery, IRequest<PagedListResponse<Entity>>
{
}

public class GetEntitiesQueryHandler : IRequestHandler<GetEntitiesQuery, PagedListResponse<Entity>>
{
    private readonly IdentityContext _context;

    public GetEntitiesQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<Entity>> Handle(GetEntitiesQuery request, CancellationToken cancellationToken) =>
        await _context.Entities.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                               request.SortColumn, request.SortOrder,
                                                               request.PageNumber, request.PageSize,
                                                               cancellationToken);
}
