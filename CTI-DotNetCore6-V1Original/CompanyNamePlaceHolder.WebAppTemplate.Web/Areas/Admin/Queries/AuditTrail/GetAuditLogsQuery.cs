using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.WebAppTemplate.Application.Common;
using CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Data;
using CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Models;
using MediatR;

namespace CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
