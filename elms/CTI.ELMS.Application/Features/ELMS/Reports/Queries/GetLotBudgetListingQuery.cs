using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Reports.Queries;

public record GetLotBudgetListingQuery : BaseQuery, IRequest<PagedListResponse<UnitBudgetState>>
{
    public string? ProjectId { get; set; }
}


public class GetLotBudgetListingQueryHandler : BaseQueryHandler<ApplicationContext, UnitBudgetState, GetLotBudgetListingQuery>, IRequestHandler<GetLotBudgetListingQuery, PagedListResponse<UnitBudgetState>>
{
    public GetLotBudgetListingQueryHandler(ApplicationContext context) : base(context)
    {

    }
    public override async Task<PagedListResponse<UnitBudgetState>> Handle(GetLotBudgetListingQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from unitBudget in Context.UnitBudget
                     select unitBudget).AsNoTracking();
        if (!string.IsNullOrEmpty(request.ProjectId))
        {
            query = query.Where(l => l.ProjectID == request.ProjectId);
        }
        return await query.Include(l => l.Unit).Include(l => l.Project)
                           .ToPagedResponse(request.SearchColumns, request.SearchValue,
                           request.SortColumn, request.SortOrder,
                           request.PageNumber, request.PageSize,
                           cancellationToken);
    }
}
