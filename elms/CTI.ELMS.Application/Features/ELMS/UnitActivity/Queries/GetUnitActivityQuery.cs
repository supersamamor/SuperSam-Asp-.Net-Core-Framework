using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitActivity.Queries;

public record GetUnitActivityQuery : BaseQuery, IRequest<PagedListResponse<UnitActivityState>>;

public class GetUnitActivityQueryHandler : BaseQueryHandler<ApplicationContext, UnitActivityState, GetUnitActivityQuery>, IRequestHandler<GetUnitActivityQuery, PagedListResponse<UnitActivityState>>
{
    public GetUnitActivityQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UnitActivityState>> Handle(GetUnitActivityQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UnitActivityState>().Include(l=>l.ActivityHistory).Include(l=>l.Unit).Include(l=>l.Activity)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
