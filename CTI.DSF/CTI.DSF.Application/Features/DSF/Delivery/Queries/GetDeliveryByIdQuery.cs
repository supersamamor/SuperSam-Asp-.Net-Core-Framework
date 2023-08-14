using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Delivery.Queries;

public record GetDeliveryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<DeliveryState>>;

public class GetDeliveryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, DeliveryState, GetDeliveryByIdQuery>, IRequestHandler<GetDeliveryByIdQuery, Option<DeliveryState>>
{
    public GetDeliveryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<DeliveryState>> Handle(GetDeliveryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Delivery.Include(l=>l.Assignment)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
