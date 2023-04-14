using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Queries;

public record GetPurchaseRequisitionByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PurchaseRequisitionState>>;

public class GetPurchaseRequisitionByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PurchaseRequisitionState, GetPurchaseRequisitionByIdQuery>, IRequestHandler<GetPurchaseRequisitionByIdQuery, Option<PurchaseRequisitionState>>
{
    public GetPurchaseRequisitionByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PurchaseRequisitionState>> Handle(GetPurchaseRequisitionByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PurchaseRequisition
			.Include(l=>l.PurchaseRequisitionItemList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
