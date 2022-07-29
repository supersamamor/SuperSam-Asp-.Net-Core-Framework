using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantLot.Queries;

public record GetTenantLotByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TenantLotState>>;

public class GetTenantLotByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TenantLotState, GetTenantLotByIdQuery>, IRequestHandler<GetTenantLotByIdQuery, Option<TenantLotState>>
{
    public GetTenantLotByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TenantLotState>> Handle(GetTenantLotByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TenantLot.Include(l=>l.Tenant)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
