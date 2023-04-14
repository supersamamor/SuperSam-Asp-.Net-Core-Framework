using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Product.Queries;

public record GetProductByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProductState>>;

public class GetProductByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProductState, GetProductByIdQuery>, IRequestHandler<GetProductByIdQuery, Option<ProductState>>
{
    public GetProductByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProductState>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Product.Include(l=>l.Brand).Include(l=>l.Item)
			.Include(l=>l.ProductImageList)
			.Include(l=>l.PurchaseRequisitionItemList)
			.Include(l=>l.SupplierQuotationItemList)
			.Include(l=>l.PurchaseItemList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
