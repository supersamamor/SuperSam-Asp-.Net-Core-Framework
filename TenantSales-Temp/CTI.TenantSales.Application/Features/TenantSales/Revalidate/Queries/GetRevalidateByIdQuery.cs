using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Revalidate.Queries;

public record GetRevalidateByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<RevalidateState>>;

public class GetRevalidateByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, RevalidateState, GetRevalidateByIdQuery>, IRequestHandler<GetRevalidateByIdQuery, Option<RevalidateState>>
{
    public GetRevalidateByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<RevalidateState>> Handle(GetRevalidateByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Revalidate.Include(l=>l.Project).Include(l=>l.Tenant)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
