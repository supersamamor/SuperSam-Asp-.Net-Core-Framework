using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Delivery.Queries;

public record GetDeliveryQuery : BaseQuery, IRequest<PagedListResponse<DeliveryState>>;

public class GetDeliveryQueryHandler : BaseQueryHandler<ApplicationContext, DeliveryState, GetDeliveryQuery>, IRequestHandler<GetDeliveryQuery, PagedListResponse<DeliveryState>>
{
    public GetDeliveryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<DeliveryState>> Handle(GetDeliveryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<DeliveryState>().Include(l=>l.Assignment)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
