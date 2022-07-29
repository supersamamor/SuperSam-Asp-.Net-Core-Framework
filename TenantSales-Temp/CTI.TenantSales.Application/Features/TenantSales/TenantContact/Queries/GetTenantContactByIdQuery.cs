using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantContact.Queries;

public record GetTenantContactByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TenantContactState>>;

public class GetTenantContactByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TenantContactState, GetTenantContactByIdQuery>, IRequestHandler<GetTenantContactByIdQuery, Option<TenantContactState>>
{
    public GetTenantContactByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TenantContactState>> Handle(GetTenantContactByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TenantContact.Include(l=>l.Tenant)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
