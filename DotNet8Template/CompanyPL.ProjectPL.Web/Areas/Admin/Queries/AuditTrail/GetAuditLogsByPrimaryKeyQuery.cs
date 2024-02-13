using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using CompanyPL.Common.Data;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
namespace CompanyPL.ProjectPL.Web.Areas.Admin.Queries.AuditTrail;
public record GetAuditLogsByPrimaryKeyQuery(string Id) : IRequest<IList<AuditLogViewModel>>;
public class GetAuditLogsByPrimaryKeyQueryHandler(ApplicationContext context) : IRequestHandler<GetAuditLogsByPrimaryKeyQuery, IList<AuditLogViewModel>>
{
    public async Task<IList<AuditLogViewModel>> Handle(GetAuditLogsByPrimaryKeyQuery request, CancellationToken cancellationToken = default)
    {
        return await context.Set<Audit>()
            .AsNoTracking().Where(l => l.PrimaryKey == request.Id).Select(e => new AuditLogViewModel()
            {
                Id = e.Id,
                UserId = e.UserId,
                Type = e.Type,
                TableName = e.TableName,
                DateTime = e.DateTime,
                PrimaryKey = e.PrimaryKey,
                TraceId = e.TraceId,
            })
            .OrderByDescending(e => e.DateTime).ToListAsync(cancellationToken: cancellationToken);
    }
}
