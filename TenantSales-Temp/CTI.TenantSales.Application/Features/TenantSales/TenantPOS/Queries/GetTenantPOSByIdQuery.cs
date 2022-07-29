using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOS.Queries;

public record GetTenantPOSByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TenantPOSState>>;

public class GetTenantPOSByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TenantPOSState, GetTenantPOSByIdQuery>, IRequestHandler<GetTenantPOSByIdQuery, Option<TenantPOSState>>
{
    public GetTenantPOSByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TenantPOSState>> Handle(GetTenantPOSByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TenantPOS.Include(l=>l.Tenant)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
