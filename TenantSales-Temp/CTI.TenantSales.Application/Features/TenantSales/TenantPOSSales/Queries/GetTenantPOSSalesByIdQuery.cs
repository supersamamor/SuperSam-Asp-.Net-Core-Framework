using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;

public record GetTenantPOSSalesByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TenantPOSSalesState>>;

public class GetTenantPOSSalesByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TenantPOSSalesState, GetTenantPOSSalesByIdQuery>, IRequestHandler<GetTenantPOSSalesByIdQuery, Option<TenantPOSSalesState>>
{
    public GetTenantPOSSalesByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TenantPOSSalesState>> Handle(GetTenantPOSSalesByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TenantPOSSales.Include(l=>l.TenantPOS)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
