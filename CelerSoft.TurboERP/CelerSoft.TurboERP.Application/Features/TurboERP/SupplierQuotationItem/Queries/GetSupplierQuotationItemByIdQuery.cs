using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Queries;

public record GetSupplierQuotationItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SupplierQuotationItemState>>;

public class GetSupplierQuotationItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SupplierQuotationItemState, GetSupplierQuotationItemByIdQuery>, IRequestHandler<GetSupplierQuotationItemByIdQuery, Option<SupplierQuotationItemState>>
{
    public GetSupplierQuotationItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SupplierQuotationItemState>> Handle(GetSupplierQuotationItemByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.SupplierQuotationItem.Include(l=>l.SupplierQuotation).Include(l=>l.Product)
			.Include(l=>l.PurchaseItemList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
