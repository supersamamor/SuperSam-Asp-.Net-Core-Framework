using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Queries;

public record GetBusinessUnitByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BusinessUnitState>>;

public class GetBusinessUnitByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BusinessUnitState, GetBusinessUnitByIdQuery>, IRequestHandler<GetBusinessUnitByIdQuery, Option<BusinessUnitState>>
{
    public GetBusinessUnitByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<BusinessUnitState>> Handle(GetBusinessUnitByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.BusinessUnit
			.Include(l=>l.ProjectBusinessUnitList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
