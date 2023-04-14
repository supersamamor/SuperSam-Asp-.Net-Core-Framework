using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Item.Queries;

public record GetItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ItemState>>;

public class GetItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ItemState, GetItemByIdQuery>, IRequestHandler<GetItemByIdQuery, Option<ItemState>>
{
    public GetItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ItemState>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Item.Include(l=>l.ItemType).Include(l=>l.Unit)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
