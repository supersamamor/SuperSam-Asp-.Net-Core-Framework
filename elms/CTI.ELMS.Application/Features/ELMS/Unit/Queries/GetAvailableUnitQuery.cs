using CTI.Common.Core.Queries;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.ELMS.Core.Constants;

namespace CTI.ELMS.Application.Features.ELMS.Unit.Queries;
public record GetAvailableUnitQuery : BaseQuery, IRequest<IList<AvailableUnitModel>>
{
    public string ProjectId { get; set; } = "";
    public string[]? SelectedUnits { get; set; }
    public string? SearchKey { get; set; }
    public DateTime? CommencementDate { get; set; }
    public string? UnitId { get; set; } = "";
}
public record AvailableUnitModel
{
    public string UnitId { get; set; } = "";
    public string UnitNo { get; set; } = "";
    public string LotArea { get; set; } = "";
    public DateTime? AvailabilityDate { get; set; }
    public string Availability
    {
        get
        {
            string ret = LotAvailability.Available;
            if (this.AvailabilityDate != null && this.AvailabilityDate >= DateTime.Today)
            {
                ret = (this.AvailabilityDate != null ? ((DateTime)this.AvailabilityDate!).ToString("MMM dd, yyyy") : "");
            }
            return ret;
        }
    }
    public int Month { get; set; }
    public decimal? January { get; init; }
    public decimal? February { get; init; }
    public decimal? March { get; init; }
    public decimal? April { get; init; }
    public decimal? May { get; init; }
    public decimal? June { get; init; }
    public decimal? July { get; init; }
    public decimal? August { get; init; }
    public decimal? September { get; init; }
    public decimal? October { get; init; }
    public decimal? November { get; init; }
    public decimal? December { get; init; }
    public decimal LotBudget
    {
        get
        {
            decimal? lotBudget = 0;
            switch (this.Month)
            {
                case 1:
                    lotBudget = this.January;
                    break;
                case 2:
                    lotBudget = this.February;
                    break;
                case 3:
                    lotBudget = this.March;
                    break;
                case 4:
                    lotBudget = this.April;
                    break;
                case 5:
                    lotBudget = this.May;
                    break;
                case 6:
                    lotBudget = this.June;
                    break;
                case 7:
                    lotBudget = this.July;
                    break;
                case 8:
                    lotBudget = this.August;
                    break;
                case 9:
                    lotBudget = this.September;
                    break;
                case 10:
                    lotBudget = this.October;
                    break;
                case 11:
                    lotBudget = this.November;
                    break;
                case 12:
                    lotBudget = this.December;
                    break;
            }
            return lotBudget == null ? 0 : (decimal)lotBudget;
        }
    }

}
public class GetAvailableUnitQueryHandler : IRequestHandler<GetAvailableUnitQuery, IList<AvailableUnitModel>>
{
    private readonly ApplicationContext _context;
    public GetAvailableUnitQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<IList<AvailableUnitModel>> Handle(GetAvailableUnitQuery request, CancellationToken cancellationToken = default)
    {
        if (request?.CommencementDate == null)
        {
            request!.CommencementDate = DateTime.Today;
        }
        int month = ((DateTime)request.CommencementDate).Month;
        int year = ((DateTime)request.CommencementDate).Year;
        var query = (from unit in _context.Unit

                     select unit).AsNoTracking();
        if (!string.IsNullOrEmpty(request.ProjectId))
        {
            query = query.Where(l => l.ProjectID == request.ProjectId);
        }
        if (request?.SelectedUnits?.Length > 0)
        {
            query = query.Where(l => !request!.SelectedUnits!.Contains(l.Id));
        }
        if (!string.IsNullOrEmpty(request?.SearchKey))
        {
            query = query.Where(l => l.UnitNo.Contains(request.SearchKey));
        }
        if (!string.IsNullOrEmpty(request?.UnitId))
        {
            query = query.Where(l => l.Id == request.UnitId);
        }
        var test = await (from unit in query.Include(l => l.UnitBudgetList)
                      join a in _context.UnitBudget on unit.Id equals a.UnitID
                      where a.Year == year
                      select new AvailableUnitModel()
                      {
                          UnitId = unit.Id,
                          UnitNo = unit.UnitNo,
                          LotArea = unit.LotArea.ToString("##,##.00"),
                          AvailabilityDate = unit.AvailabilityDate,
                          Month = month,
                          January = a.January,
                          February = a.February,
                          March = a.March,
                          April = a.April,
                          May = a.May,
                          June = a.June,
                          July = a.July,
                          August = a.August,
                          September = a.September,
                          October = a.October,
                          November = a.November,
                          December = a.December,
                      })
                      .ToListAsync(cancellationToken: cancellationToken);
        return await (from unit in query.Include(l => l.UnitBudgetList)
                      join a in _context.UnitBudget on unit.Id equals a.UnitID
                      where a.Year == year
                      select new AvailableUnitModel()
                      {
                          UnitId = unit.Id,
                          UnitNo = unit.UnitNo,
                          LotArea = unit.LotArea.ToString("##,##.00"),
                          AvailabilityDate = unit.AvailabilityDate,
                          Month = month,
                          January = a.January,
                          February = a.February,
                          March = a.March,
                          April = a.April,
                          May = a.May,
                          June = a.June,
                          July = a.July,
                          August = a.August,
                          September = a.September,
                          October = a.October,
                          November = a.November,
                          December = a.December,
                      })
                      .ToListAsync(cancellationToken: cancellationToken);
    }
}
