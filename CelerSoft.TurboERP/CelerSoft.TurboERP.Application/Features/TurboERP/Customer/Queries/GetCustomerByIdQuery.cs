using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Queries;

public record GetCustomerByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CustomerState>>;

public class GetCustomerByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CustomerState, GetCustomerByIdQuery>, IRequestHandler<GetCustomerByIdQuery, Option<CustomerState>>
{
    public GetCustomerByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CustomerState>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Customer
			.Include(l=>l.CustomerContactPersonList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
