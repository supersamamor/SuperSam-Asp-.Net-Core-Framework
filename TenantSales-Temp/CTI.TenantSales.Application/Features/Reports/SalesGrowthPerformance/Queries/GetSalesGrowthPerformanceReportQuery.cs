using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace CTI.TenantSales.Application.Features.Reports.SalesGrowthPerformance.Queries;

public record GetSalesGrowthPerformanceReportQuery(string? ProjectId, int Year, int Month) : IRequest<IList<ThemeState>>;
public class GetSalesGrowthPerformanceReportQueryHandler : IRequestHandler<GetSalesGrowthPerformanceReportQuery, IList<ThemeState>>
{
    private readonly ApplicationContext _context;
    public GetSalesGrowthPerformanceReportQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<IList<ThemeState>> Handle(GetSalesGrowthPerformanceReportQuery request, CancellationToken cancellationToken = default)
    {
        var query = _context.Theme
          .IncludeFilter(a => a.ClassificationList!.Select(b => b))
          .IncludeFilter(a => a.ClassificationList!.SelectMany(c => c.CategoryList!))
          .IncludeFilter(a => a.ClassificationList!.SelectMany(c => c.CategoryList!.SelectMany(d => d.TenantList!.Where(l => l.ProjectId == request.ProjectId))))
          .IncludeFilter(a => a.ClassificationList!.SelectMany(c => c.CategoryList!.SelectMany(d => d.TenantList!.SelectMany(e => e.ReportSalesGrowthPerformanceMonthList!.Where(m => m.Year == request.Year && m.Month == request.Month)))))
          .IncludeFilter(a => a.ClassificationList!.SelectMany(c => c.CategoryList!.SelectMany(d => d.TenantList!.SelectMany(e => e.TenantLotMonthYearList!.Where(l => l.Year == request.Year && l.Month == request.Month)))))
          .IncludeFilter(a => a.ClassificationList!.SelectMany(c => c.CategoryList!.SelectMany(d => d.TenantList!).Select(l => l.Project)));
        return await query.ToListAsync(cancellationToken);
    }
}
