using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitGroup.Queries;

public record GetUnitGroupQuery : BaseQuery, IRequest<PagedListResponse<UnitGroupState>>;

public class GetUnitGroupQueryHandler : BaseQueryHandler<ApplicationContext, UnitGroupState, GetUnitGroupQuery>, IRequestHandler<GetUnitGroupQuery, PagedListResponse<UnitGroupState>>
{
    public GetUnitGroupQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UnitGroupState>> Handle(GetUnitGroupQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UnitGroupState>().Include(l=>l.OfferingHistory)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
