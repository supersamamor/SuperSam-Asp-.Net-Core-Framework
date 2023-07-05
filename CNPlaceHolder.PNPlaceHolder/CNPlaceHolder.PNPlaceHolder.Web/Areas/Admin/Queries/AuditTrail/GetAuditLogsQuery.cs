using CNPlaceHolder.Common.Utility.Models;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using MediatR;
using CNPlaceHolder.Common.Core.Queries;
using CNPlaceHolder.Common.Data;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
