using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Queries;

public record GetReportByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportState>>;

public class GetReportByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportState, GetReportByIdQuery>, IRequestHandler<GetReportByIdQuery, Option<ReportState>>
{
    public GetReportByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }

    public override async Task<Option<ReportState>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.Report
            .Include(l => l.ReportTableList)
            .Include(l => l.ReportTableJoinParameterList)
            .Include(l => l.ReportColumnHeaderList)
            .Include(l => l.ReportFilterGroupingList)
            .Include(l => l.ReportQueryFilterList)
            .Include(l => l.ReportRoleAssignmentList)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}
