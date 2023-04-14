using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Data;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
