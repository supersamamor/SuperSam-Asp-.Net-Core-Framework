using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Queries;

public record GetPreSelectedUnitQuery : BaseQuery, IRequest<PagedListResponse<PreSelectedUnitState>>;

public class GetPreSelectedUnitQueryHandler : BaseQueryHandler<ApplicationContext, PreSelectedUnitState, GetPreSelectedUnitQuery>, IRequestHandler<GetPreSelectedUnitQuery, PagedListResponse<PreSelectedUnitState>>
{
    public GetPreSelectedUnitQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<PreSelectedUnitState>> Handle(GetPreSelectedUnitQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<PreSelectedUnitState>().Include(l=>l.Offering).Include(l=>l.Unit)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
