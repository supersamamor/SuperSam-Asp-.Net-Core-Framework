using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Queries;

public record GetUnitOfferedHistoryQuery : BaseQuery, IRequest<PagedListResponse<UnitOfferedHistoryState>>;

public class GetUnitOfferedHistoryQueryHandler : BaseQueryHandler<ApplicationContext, UnitOfferedHistoryState, GetUnitOfferedHistoryQuery>, IRequestHandler<GetUnitOfferedHistoryQuery, PagedListResponse<UnitOfferedHistoryState>>
{
    public GetUnitOfferedHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UnitOfferedHistoryState>> Handle(GetUnitOfferedHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UnitOfferedHistoryState>().Include(l=>l.OfferingHistory).Include(l=>l.Unit).Include(l=>l.Offering)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
