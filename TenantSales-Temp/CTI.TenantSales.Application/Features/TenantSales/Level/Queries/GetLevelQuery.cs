using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Level.Queries;

public record GetLevelQuery : BaseQuery, IRequest<PagedListResponse<LevelState>>
{
    public string? ProjectId { get; set; }
}

public class GetLevelQueryHandler : BaseQueryHandler<ApplicationContext, LevelState, GetLevelQuery>, IRequestHandler<GetLevelQuery, PagedListResponse<LevelState>>
{
    public GetLevelQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<LevelState>> Handle(GetLevelQuery request, CancellationToken cancellationToken = default) =>
        await Context.Set<LevelState>().Include(l => l.Project)
         .Where(l => (!string.IsNullOrEmpty(request.ProjectId) && l.ProjectId == request.ProjectId) || (string.IsNullOrEmpty(request.ProjectId))).AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
            request.SortColumn, request.SortOrder,
            request.PageNumber, request.PageSize,
            cancellationToken);
}
