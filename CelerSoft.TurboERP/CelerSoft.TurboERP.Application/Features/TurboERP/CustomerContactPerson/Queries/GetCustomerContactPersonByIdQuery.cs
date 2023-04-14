using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Queries;

public record GetCustomerContactPersonByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CustomerContactPersonState>>;

public class GetCustomerContactPersonByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CustomerContactPersonState, GetCustomerContactPersonByIdQuery>, IRequestHandler<GetCustomerContactPersonByIdQuery, Option<CustomerContactPersonState>>
{
    public GetCustomerContactPersonByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CustomerContactPersonState>> Handle(GetCustomerContactPersonByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.CustomerContactPerson.Include(l=>l.Customer)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
