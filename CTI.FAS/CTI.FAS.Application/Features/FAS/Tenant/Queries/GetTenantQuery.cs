using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Tenant.Queries;

public record GetTenantQuery : BaseQuery, IRequest<PagedListResponse<TenantState>>;

public class GetTenantQueryHandler : BaseQueryHandler<ApplicationContext, TenantState, GetTenantQuery>, IRequestHandler<GetTenantQuery, PagedListResponse<TenantState>>
{
    public GetTenantQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TenantState>> Handle(GetTenantQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TenantState>().Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
