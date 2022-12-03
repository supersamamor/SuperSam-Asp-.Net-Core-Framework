using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Tenant.Queries;

public record GetTenantByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TenantState>>;

public class GetTenantByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TenantState, GetTenantByIdQuery>, IRequestHandler<GetTenantByIdQuery, Option<TenantState>>
{
    public GetTenantByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TenantState>> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Tenant.Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
