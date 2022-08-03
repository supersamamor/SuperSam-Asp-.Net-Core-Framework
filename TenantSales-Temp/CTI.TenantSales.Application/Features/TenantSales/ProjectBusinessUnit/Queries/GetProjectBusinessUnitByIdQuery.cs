using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Queries;

public record GetProjectBusinessUnitByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectBusinessUnitState>>;

public class GetProjectBusinessUnitByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectBusinessUnitState, GetProjectBusinessUnitByIdQuery>, IRequestHandler<GetProjectBusinessUnitByIdQuery, Option<ProjectBusinessUnitState>>
{
    public GetProjectBusinessUnitByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectBusinessUnitState>> Handle(GetProjectBusinessUnitByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectBusinessUnit.Include(l=>l.Project).Include(l=>l.BusinessUnit)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
