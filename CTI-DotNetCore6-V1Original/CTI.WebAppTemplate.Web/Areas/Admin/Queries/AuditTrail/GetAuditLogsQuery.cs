using CTI.Common.Utility.Models;
using CTI.WebAppTemplate.Application.Common;
using CTI.WebAppTemplate.Infrastructure.Data;
using CTI.WebAppTemplate.Infrastructure.Models;
using MediatR;

namespace CTI.WebAppTemplate.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
