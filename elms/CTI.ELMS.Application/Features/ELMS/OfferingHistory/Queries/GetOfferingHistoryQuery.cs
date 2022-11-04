using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.OfferingHistory.Queries;

public record GetOfferingHistoryQuery : BaseQuery, IRequest<PagedListResponse<OfferingHistoryState>>;

public class GetOfferingHistoryQueryHandler : BaseQueryHandler<ApplicationContext, OfferingHistoryState, GetOfferingHistoryQuery>, IRequestHandler<GetOfferingHistoryQuery, PagedListResponse<OfferingHistoryState>>
{
    public GetOfferingHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<OfferingHistoryState>> Handle(GetOfferingHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<OfferingHistoryState>().Include(l=>l.Lead).Include(l=>l.Offering).Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
