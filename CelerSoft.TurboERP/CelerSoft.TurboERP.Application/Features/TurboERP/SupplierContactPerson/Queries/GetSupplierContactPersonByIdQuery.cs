using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Queries;

public record GetSupplierContactPersonByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SupplierContactPersonState>>;

public class GetSupplierContactPersonByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SupplierContactPersonState, GetSupplierContactPersonByIdQuery>, IRequestHandler<GetSupplierContactPersonByIdQuery, Option<SupplierContactPersonState>>
{
    public GetSupplierContactPersonByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SupplierContactPersonState>> Handle(GetSupplierContactPersonByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.SupplierContactPerson.Include(l=>l.Supplier)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
