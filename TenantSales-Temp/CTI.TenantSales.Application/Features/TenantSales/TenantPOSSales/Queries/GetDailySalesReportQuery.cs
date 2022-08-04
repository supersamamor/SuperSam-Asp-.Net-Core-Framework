using AutoMapper;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;

public record GetDailySalesReportQuery(DateTime DateFrom, DateTime DateTo, string? TenantId, string? LevelId, string? ProjectId) : IRequest<IList<TenantState>>;
public class GetDailySalesReportQueryHandler : IRequestHandler<GetDailySalesReportQuery, IList<TenantState>>
{
    private readonly ApplicationContext _context;
    public GetDailySalesReportQueryHandler(ApplicationContext context)
    {
        _context = context;  
    }
    public async Task<IList<TenantState>> Handle(GetDailySalesReportQuery request, CancellationToken cancellationToken = default)
    {
        var query = _context.Tenant
            .Include(l => l.Project)
            .Include(l => l.Level)
            .IncludeFilter(a => a.TenantPOSList!.SelectMany(b => b.TenantPOSSalesList!.Where(c => c.SalesDate >= request.DateFrom && c.SalesDate <= request.DateTo)));

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
