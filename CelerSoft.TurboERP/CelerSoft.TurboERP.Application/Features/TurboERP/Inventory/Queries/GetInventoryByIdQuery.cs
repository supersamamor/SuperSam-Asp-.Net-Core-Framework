using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Queries;

public record GetInventoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<InventoryState>>;

public class GetInventoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, InventoryState, GetInventoryByIdQuery>, IRequestHandler<GetInventoryByIdQuery, Option<InventoryState>>
{
    public GetInventoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<InventoryState>> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Inventory.Include(l=>l.PurchaseItem).Include(l=>l.Product)
			.Include(l=>l.InventoryHistoryList)
			.Include(l=>l.OrderItemList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
