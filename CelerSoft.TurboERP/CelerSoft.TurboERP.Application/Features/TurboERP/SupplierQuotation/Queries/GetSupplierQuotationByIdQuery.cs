using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Queries;

public record GetSupplierQuotationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SupplierQuotationState>>;

public class GetSupplierQuotationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SupplierQuotationState, GetSupplierQuotationByIdQuery>, IRequestHandler<GetSupplierQuotationByIdQuery, Option<SupplierQuotationState>>
{
    public GetSupplierQuotationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SupplierQuotationState>> Handle(GetSupplierQuotationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.SupplierQuotation.Include(l=>l.Supplier).Include(l=>l.PurchaseRequisition)
			.Include(l=>l.SupplierQuotationItemList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
