using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Queries;

public record GetPurchaseItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PurchaseItemState>>;

public class GetPurchaseItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PurchaseItemState, GetPurchaseItemByIdQuery>, IRequestHandler<GetPurchaseItemByIdQuery, Option<PurchaseItemState>>
{
    public GetPurchaseItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PurchaseItemState>> Handle(GetPurchaseItemByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PurchaseItem.Include(l=>l.Product).Include(l=>l.SupplierQuotationItem)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
