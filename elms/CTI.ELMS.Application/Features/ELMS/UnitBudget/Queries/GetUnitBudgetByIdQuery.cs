using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitBudget.Queries;

public record GetUnitBudgetByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UnitBudgetState>>;

public class GetUnitBudgetByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UnitBudgetState, GetUnitBudgetByIdQuery>, IRequestHandler<GetUnitBudgetByIdQuery, Option<UnitBudgetState>>
{
    public GetUnitBudgetByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UnitBudgetState>> Handle(GetUnitBudgetByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UnitBudget.Include(l=>l.UnitBudget).Include(l=>l.Unit).Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
