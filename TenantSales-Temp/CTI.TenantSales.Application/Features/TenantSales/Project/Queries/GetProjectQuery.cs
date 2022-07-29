using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Project.Queries;

public record GetProjectQuery : BaseQuery, IRequest<PagedListResponse<ProjectState>>;

public class GetProjectQueryHandler : BaseQueryHandler<ApplicationContext, ProjectState, GetProjectQuery>, IRequestHandler<GetProjectQuery, PagedListResponse<ProjectState>>
{
    public GetProjectQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectState>> Handle(GetProjectQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectState>().Include(l=>l.Company)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
