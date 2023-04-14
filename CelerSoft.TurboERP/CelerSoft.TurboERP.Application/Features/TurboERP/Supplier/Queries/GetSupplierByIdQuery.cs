using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Queries;

public record GetSupplierByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SupplierState>>;

public class GetSupplierByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SupplierState, GetSupplierByIdQuery>, IRequestHandler<GetSupplierByIdQuery, Option<SupplierState>>
{
    public GetSupplierByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SupplierState>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Supplier
			.Include(l=>l.SupplierContactPersonList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
