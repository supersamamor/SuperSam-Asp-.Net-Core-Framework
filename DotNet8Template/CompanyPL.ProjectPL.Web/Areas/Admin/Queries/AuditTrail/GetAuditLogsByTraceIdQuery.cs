using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using CompanyPL.Common.Data;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
namespace CompanyPL.ProjectPL.Web.Areas.Admin.Queries.AuditTrail;
public record GetAuditLogsByTraceIdQuery(string TraceId, string MainRecordId) : IRequest<IList<AuditLogViewModel>>;
public class GetAuditLogsByTraceIdQueryHandler(ApplicationContext context) : IRequestHandler<GetAuditLogsByTraceIdQuery, IList<AuditLogViewModel>>
{
    public async Task<IList<AuditLogViewModel>> Handle(GetAuditLogsByTraceIdQuery request, CancellationToken cancellationToken = default)
    {
        return await context.Set<Audit>()
            .AsNoTracking().Where(l => l.TraceId == request.TraceId && l.PrimaryKey != request.MainRecordId).Select(e => new AuditLogViewModel()
            {
                Id = e.Id,
                UserId = e.UserId,
                Type = e.Type,
                TableName = e.TableName,
                DateTime = e.DateTime,
                PrimaryKey = e.PrimaryKey,
                TraceId = e.TraceId,
                OldValues = e.OldValues,
                NewValues = e.NewValues,
            })
            .OrderBy(e => e.TableName).ThenBy(e => e.Type).ToListAsync(cancellationToken: cancellationToken);
    }
}
