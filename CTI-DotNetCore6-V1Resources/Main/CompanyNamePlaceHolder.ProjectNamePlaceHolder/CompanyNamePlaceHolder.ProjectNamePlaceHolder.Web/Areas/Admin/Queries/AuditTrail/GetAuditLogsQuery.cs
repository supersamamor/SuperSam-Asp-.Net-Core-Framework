using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Models;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.AuditTrail;

public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
{
    public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
}
