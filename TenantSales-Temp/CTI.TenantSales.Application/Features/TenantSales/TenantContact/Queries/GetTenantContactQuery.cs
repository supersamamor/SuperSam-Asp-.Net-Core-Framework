using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantContact.Queries;

public record GetTenantContactQuery : BaseQuery, IRequest<PagedListResponse<TenantContactState>>;

public class GetTenantContactQueryHandler : BaseQueryHandler<ApplicationContext, TenantContactState, GetTenantContactQuery>, IRequestHandler<GetTenantContactQuery, PagedListResponse<TenantContactState>>
{
    public GetTenantContactQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TenantContactState>> Handle(GetTenantContactQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TenantContactState>().Include(l=>l.Tenant)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
