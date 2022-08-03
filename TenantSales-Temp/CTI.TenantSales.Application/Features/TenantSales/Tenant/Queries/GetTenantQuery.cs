using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Tenant.Queries;

public record GetTenantQuery : BaseQuery, IRequest<PagedListResponse<TenantState>>
{
	public string? ProjectId { get; set; }
	public string? LevelId { get; set; }
}

public class GetTenantQueryHandler : BaseQueryHandler<ApplicationContext, TenantState, GetTenantQuery>, IRequestHandler<GetTenantQuery, PagedListResponse<TenantState>>
{
    public GetTenantQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TenantState>> Handle(GetTenantQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TenantState>().Include(l=>l.Project).Include(l=>l.Level).Include(l=>l.RentalType)
		.Where(l => ((!string.IsNullOrEmpty(request.LevelId) && l.LevelId == request.LevelId) || (string.IsNullOrEmpty(request.LevelId)))
		&& ((!string.IsNullOrEmpty(request.ProjectId) && l.ProjectId == request.ProjectId) || (string.IsNullOrEmpty(request.ProjectId)))
		).AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
