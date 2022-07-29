using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Queries;

public record GetSalesCategoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SalesCategoryState>>;

public class GetSalesCategoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SalesCategoryState, GetSalesCategoryByIdQuery>, IRequestHandler<GetSalesCategoryByIdQuery, Option<SalesCategoryState>>
{
    public GetSalesCategoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SalesCategoryState>> Handle(GetSalesCategoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.SalesCategory.Include(l=>l.Tenant)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
