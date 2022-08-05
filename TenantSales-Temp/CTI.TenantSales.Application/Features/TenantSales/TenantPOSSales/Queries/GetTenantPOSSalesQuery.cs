using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;

public record GetTenantPOSSalesQuery : BaseQuery, IRequest<PagedListResponse<TenantPOSSalesState>>
{
    public string? TenantId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}
public class GetTenantPOSSalesQueryHandler : BaseQueryHandler<ApplicationContext, TenantPOSSalesState, GetTenantPOSSalesQuery>, IRequestHandler<GetTenantPOSSalesQuery, PagedListResponse<TenantPOSSalesState>>
{
    public GetTenantPOSSalesQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<TenantPOSSalesState>> Handle(GetTenantPOSSalesQuery request, CancellationToken cancellationToken = default) =>
        await Context.Set<TenantPOSSalesState>().Include(l => l.TenantPOS).ThenInclude(l => l!.Tenant)
        .Where(c => c.SalesType == Convert.ToInt32(Core.Constants.SalesTypeEnum.Daily))
        .Where(c => c.SalesDate >= request.DateFrom
                    && c.SalesDate <= request.DateTo)
        .Where(l => l.TenantPOS!.TenantId == request.TenantId).AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
            request.SortColumn, request.SortOrder,
            request.PageNumber, request.PageSize,
            cancellationToken);
}
