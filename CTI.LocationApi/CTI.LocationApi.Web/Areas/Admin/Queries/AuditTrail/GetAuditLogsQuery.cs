using CTI.Common.Utility.Models;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Core.Queries;
using CTI.Common.Data;

namespace CTI.LocationApi.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
