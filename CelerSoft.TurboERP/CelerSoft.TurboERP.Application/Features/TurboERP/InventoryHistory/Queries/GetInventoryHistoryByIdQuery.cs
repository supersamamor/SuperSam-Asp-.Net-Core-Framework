using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Queries;

public record GetInventoryHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<InventoryHistoryState>>;

public class GetInventoryHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, InventoryHistoryState, GetInventoryHistoryByIdQuery>, IRequestHandler<GetInventoryHistoryByIdQuery, Option<InventoryHistoryState>>
{
    public GetInventoryHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<InventoryHistoryState>> Handle(GetInventoryHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.InventoryHistory.Include(l=>l.Inventory)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
