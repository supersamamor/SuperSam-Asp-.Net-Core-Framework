using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Queries;

public record GetShoppingCartByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ShoppingCartState>>;

public class GetShoppingCartByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ShoppingCartState, GetShoppingCartByIdQuery>, IRequestHandler<GetShoppingCartByIdQuery, Option<ShoppingCartState>>
{
    public GetShoppingCartByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ShoppingCartState>> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ShoppingCart.Include(l=>l.Inventory)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
