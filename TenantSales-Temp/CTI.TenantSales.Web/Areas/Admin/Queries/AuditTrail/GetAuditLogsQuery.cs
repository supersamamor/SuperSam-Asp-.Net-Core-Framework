using CTI.Common.Utility.Models;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Core.Queries;
using CTI.Common.Data;

namespace CTI.TenantSales.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
