using CTI.Common.Utility.Extensions;
using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Core.Queries;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;

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
