using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Category.Queries;

public record GetCategoryQuery : BaseQuery, IRequest<PagedListResponse<CategoryState>>;

public class GetCategoryQueryHandler : BaseQueryHandler<ApplicationContext, CategoryState, GetCategoryQuery>, IRequestHandler<GetCategoryQuery, PagedListResponse<CategoryState>>
{
    public GetCategoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CategoryState>> Handle(GetCategoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CategoryState>().Include(l=>l.Classification)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
