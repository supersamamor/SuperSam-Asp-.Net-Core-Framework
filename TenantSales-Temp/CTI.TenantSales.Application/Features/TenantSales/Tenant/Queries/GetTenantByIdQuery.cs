using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Tenant.Queries;

public record GetTenantByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TenantState>>;

public class GetTenantByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TenantState, GetTenantByIdQuery>, IRequestHandler<GetTenantByIdQuery, Option<TenantState>>
{
    public GetTenantByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TenantState>> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Tenant.Include(l=>l.Level).Include(l=>l.Project).Include(l=>l.RentalType)
			.Include(l=>l.TenantLotList)
			.Include(l=>l.SalesCategoryList)
			.Include(l=>l.TenantContactList)
			.Include(l=>l.TenantPOSList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
