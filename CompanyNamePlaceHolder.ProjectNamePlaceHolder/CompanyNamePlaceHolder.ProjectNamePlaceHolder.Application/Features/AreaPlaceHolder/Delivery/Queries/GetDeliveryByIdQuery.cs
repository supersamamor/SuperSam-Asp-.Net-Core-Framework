using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Queries;

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
