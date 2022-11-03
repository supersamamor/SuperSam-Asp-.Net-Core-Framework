using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrementHistory.Queries;

public record GetAnnualIncrementHistoryQuery : BaseQuery, IRequest<PagedListResponse<AnnualIncrementHistoryState>>;

public class GetAnnualIncrementHistoryQueryHandler : BaseQueryHandler<ApplicationContext, AnnualIncrementHistoryState, GetAnnualIncrementHistoryQuery>, IRequestHandler<GetAnnualIncrementHistoryQuery, PagedListResponse<AnnualIncrementHistoryState>>
{
    public GetAnnualIncrementHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<AnnualIncrementHistoryState>> Handle(GetAnnualIncrementHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<AnnualIncrementHistoryState>().Include(l=>l.UnitOfferedHistory)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
