using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Queries;

public record GetProjectBusinessUnitQuery : BaseQuery, IRequest<PagedListResponse<ProjectBusinessUnitState>>;

public class GetProjectBusinessUnitQueryHandler : BaseQueryHandler<ApplicationContext, ProjectBusinessUnitState, GetProjectBusinessUnitQuery>, IRequestHandler<GetProjectBusinessUnitQuery, PagedListResponse<ProjectBusinessUnitState>>
{
    public GetProjectBusinessUnitQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectBusinessUnitState>> Handle(GetProjectBusinessUnitQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectBusinessUnitState>().Include(l=>l.Project).Include(l=>l.BusinessUnit)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
