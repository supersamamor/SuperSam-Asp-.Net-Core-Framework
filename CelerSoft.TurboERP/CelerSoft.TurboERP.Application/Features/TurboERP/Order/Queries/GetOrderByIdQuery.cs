using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Order.Queries;

public record GetOrderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<OrderState>>;

public class GetOrderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, OrderState, GetOrderByIdQuery>, IRequestHandler<GetOrderByIdQuery, Option<OrderState>>
{
    public GetOrderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<OrderState>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Order.Include(l=>l.Customer)
			.Include(l=>l.OrderItemList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
