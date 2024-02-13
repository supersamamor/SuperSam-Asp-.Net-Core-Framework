using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using CompanyPL.Common.Data;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
using CompanyPL.Common.Utility.Models;
using CompanyPL.Common.Core.Queries;
using CompanyPL.ProjectPL.Application.DTOs;
namespace CompanyPL.ProjectPL.Web.Areas.Admin.Queries.AuditTrail;
public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<AuditLogViewModel>>;
public class GetAuditLogsQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, AuditLogViewModel, GetAuditLogsQuery>(context), IRequestHandler<GetAuditLogsQuery, PagedListResponse<AuditLogViewModel>>
{
    public override async Task<PagedListResponse<AuditLogViewModel>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Audit>()
            .AsNoTracking().Select(e => new AuditLogViewModel()
            {
                Id = e.Id,
                UserId = e.UserId,
                Type = e.Type,
                TableName = e.TableName,
                DateTime = e.DateTime,
                PrimaryKey = e.PrimaryKey,
                TraceId = e.TraceId,
            })
            .ToPagedResponse(request.SearchColumns, request.SearchValue,
                request.SortColumn, request.SortOrder,
                request.PageNumber, request.PageSize,
                cancellationToken);
    }
}
