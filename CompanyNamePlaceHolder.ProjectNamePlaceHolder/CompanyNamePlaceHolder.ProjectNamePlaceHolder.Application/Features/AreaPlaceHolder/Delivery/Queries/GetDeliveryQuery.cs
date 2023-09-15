using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Queries;

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
