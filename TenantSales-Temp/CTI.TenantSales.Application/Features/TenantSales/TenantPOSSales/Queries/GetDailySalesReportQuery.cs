using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;

public record GetDailySalesReportQuery(DateTime DateFrom, DateTime DateTo, string? TenantId, string? LevelId, string? ProjectId) : IRequest<IList<TenantPOSSalesState>>;
public class GetDailySalesReportQueryHandler : IRequestHandler<GetDailySalesReportQuery, IList<TenantPOSSalesState>>
{
    private readonly ApplicationContext _context;
    public GetDailySalesReportQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<IList<TenantPOSSalesState>> Handle(GetDailySalesReportQuery request, CancellationToken cancellationToken = default)
    {
        var query = _context.TenantPOSSales
            .Include(l => l.TenantPOS).ThenInclude(l => l!.Tenant).ThenInclude(l => l!.Project).AsNoTracking();
        if (request.DateFrom != Convert.ToDateTime(DateTime.MinValue))
        {
            query = query.Where(l => l.SalesDate >= request.DateFrom);
        }

        if (request.DateTo != Convert.ToDateTime(DateTime.MinValue))
        {
            query = query.Where(l => l.SalesDate <= request.DateTo);
        }
        if (!string.IsNullOrEmpty(request.TenantId))
        {
            query = query.Where(l => l.TenantPOS!.TenantId == request.TenantId);
        }
        else if (!string.IsNullOrEmpty(request.LevelId))
        {
            query = query.Where(l => l.TenantPOS!.Tenant!.LevelId == request.LevelId);
        }
        else if (!string.IsNullOrEmpty(request.ProjectId))
        {
            query = query.Where(l => l.TenantPOS!.Tenant!.ProjectId == request.ProjectId);
        }
        return await query.OrderBy(l => l.TenantPOS!.Tenant!.Project!.Name)
            .ThenBy(l => l.TenantPOS!.Tenant!.Name)
             .ThenBy(l => l.TenantPOS!.Code)
             .ThenBy(l => l.SalesCategory)
             .ThenBy(l => l.SalesDate).ToListAsync(cancellationToken: cancellationToken);
    }
}
