using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Queries;

public record GetPurchaseByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PurchaseState>>;

public class GetPurchaseByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PurchaseState, GetPurchaseByIdQuery>, IRequestHandler<GetPurchaseByIdQuery, Option<PurchaseState>>
{
    public GetPurchaseByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PurchaseState>> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Purchase.Include(l=>l.PurchaseRequisition).Include(l=>l.SupplierQuotation)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
