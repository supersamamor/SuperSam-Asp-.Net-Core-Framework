using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.AuditTrail;
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
