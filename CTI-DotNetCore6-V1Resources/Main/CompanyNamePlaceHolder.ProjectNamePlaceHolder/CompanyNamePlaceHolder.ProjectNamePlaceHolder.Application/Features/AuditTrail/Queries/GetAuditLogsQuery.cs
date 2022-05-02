using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using MediatR;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AuditTrail.Queries
{
    public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<Audit>>;

    public class GetAuditLogsQueryHandler : BaseQueryHandler<ApplicationContext, Audit, GetAuditLogsQuery>, IRequestHandler<GetAuditLogsQuery, PagedListResponse<Audit>>
    {
        public GetAuditLogsQueryHandler(ApplicationContext context) : base(context) { }
    }
}
