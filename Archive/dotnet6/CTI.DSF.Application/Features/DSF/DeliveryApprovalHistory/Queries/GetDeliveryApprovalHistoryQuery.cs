using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Queries;

public record GetDeliveryApprovalHistoryQuery : BaseQuery, IRequest<PagedListResponse<DeliveryApprovalHistoryState>>;

public class GetDeliveryApprovalHistoryQueryHandler : BaseQueryHandler<ApplicationContext, DeliveryApprovalHistoryState, GetDeliveryApprovalHistoryQuery>, IRequestHandler<GetDeliveryApprovalHistoryQuery, PagedListResponse<DeliveryApprovalHistoryState>>
{
    public GetDeliveryApprovalHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<DeliveryApprovalHistoryState>> Handle(GetDeliveryApprovalHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<DeliveryApprovalHistoryState>().Include(l=>l.Delivery)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
