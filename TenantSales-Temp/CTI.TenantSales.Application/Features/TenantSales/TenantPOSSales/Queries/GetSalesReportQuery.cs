using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;

public record GetSalesReportQuery(DateTime DateFrom, DateTime DateTo, string? ProjectId, string? LevelId, string? TenantId, string? SalesCategoryCode = null) : IRequest<IList<TenantState>>;
public class GetDailySalesReportQueryHandler : IRequestHandler<GetSalesReportQuery, IList<TenantState>>
{
    private readonly ApplicationContext _context;
    public GetDailySalesReportQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<IList<TenantState>> Handle(GetSalesReportQuery request, CancellationToken cancellationToken = default)
    {
        var query = _context.Tenant
            .Include(l => l.Project)
            .ThenInclude(l => l!.Company).ThenInclude(l => l!.DatabaseConnectionSetup)
            .Include(l => l.Level)
            .IncludeFilter(a =>
                a.TenantPOSList!.SelectMany(b => b.TenantPOSSalesList!
                    .Where(c => (c.SalesDate >= request.DateFrom
                    && c.SalesDate <= request.DateTo
                    && c.SalesType == Convert.ToInt32(Core.Constants.SalesTypeEnum.Daily))
                    && (string.IsNullOrEmpty(request.SalesCategoryCode) || (!string.IsNullOrEmpty(request.SalesCategoryCode) && c.SalesCategory == request.SalesCategoryCode))
                    )));
        if (!string.IsNullOrEmpty(request.TenantId))
        {
            query = query.Where(l => l.Id == request.TenantId);
        }
        else if (!string.IsNullOrEmpty(request.LevelId))
        {
            query = query.Where(l => l.LevelId == request.LevelId);
        }
        else if (!string.IsNullOrEmpty(request.ProjectId))
        {
            query = query.Where(l => l.ProjectId == request.ProjectId);
        }
        return await query.OrderBy(l => l.Project!.Name)
           .ThenBy(l => l.Name).ToListAsync(cancellationToken: cancellationToken);
    }
}
