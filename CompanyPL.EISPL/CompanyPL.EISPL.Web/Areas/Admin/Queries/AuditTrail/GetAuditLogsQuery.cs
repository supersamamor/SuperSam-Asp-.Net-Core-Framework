using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Data;

namespace CompanyPL.EISPL.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
