using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Queries;

public record GetPurchaseRequisitionItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PurchaseRequisitionItemState>>;

public class GetPurchaseRequisitionItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PurchaseRequisitionItemState, GetPurchaseRequisitionItemByIdQuery>, IRequestHandler<GetPurchaseRequisitionItemByIdQuery, Option<PurchaseRequisitionItemState>>
{
    public GetPurchaseRequisitionItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PurchaseRequisitionItemState>> Handle(GetPurchaseRequisitionItemByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PurchaseRequisitionItem.Include(l=>l.PurchaseRequisition).Include(l=>l.Product)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
