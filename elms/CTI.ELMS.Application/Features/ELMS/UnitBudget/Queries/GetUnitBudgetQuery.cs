using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UnitBudget.Queries;

public record GetUnitBudgetQuery : BaseQuery, IRequest<PagedListResponse<UnitBudgetState>>;

public class GetUnitBudgetQueryHandler : BaseQueryHandler<ApplicationContext, UnitBudgetState, GetUnitBudgetQuery>, IRequestHandler<GetUnitBudgetQuery, PagedListResponse<UnitBudgetState>>
{
    public GetUnitBudgetQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UnitBudgetState>> Handle(GetUnitBudgetQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UnitBudgetState>().Include(l=>l.Unit).Include(l=>l.Project).Include(l=>l.UnitBudget)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
