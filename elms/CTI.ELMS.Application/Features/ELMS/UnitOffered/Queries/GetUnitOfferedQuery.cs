using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitOffered.Queries;

public record GetUnitOfferedQuery : BaseQuery, IRequest<PagedListResponse<UnitOfferedState>>;

public class GetUnitOfferedQueryHandler : BaseQueryHandler<ApplicationContext, UnitOfferedState, GetUnitOfferedQuery>, IRequestHandler<GetUnitOfferedQuery, PagedListResponse<UnitOfferedState>>
{
    public GetUnitOfferedQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UnitOfferedState>> Handle(GetUnitOfferedQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UnitOfferedState>().Include(l=>l.Offering).Include(l=>l.Unit)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
