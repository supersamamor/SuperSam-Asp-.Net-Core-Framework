using CelerSoft.Common.Utility.Extensions;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.Identity;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Queries.Entities;

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
