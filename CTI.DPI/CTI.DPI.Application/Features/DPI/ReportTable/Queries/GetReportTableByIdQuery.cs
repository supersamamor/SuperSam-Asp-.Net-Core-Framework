using CTI.Common.Core.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportTable.Queries;

public record GetReportTableByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportTableState>>;

public class GetReportTableByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportTableState, GetReportTableByIdQuery>, IRequestHandler<GetReportTableByIdQuery, Option<ReportTableState>>
{
    public GetReportTableByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportTableState>> Handle(GetReportTableByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportTable.Include(l=>l.Report)
			.Include(l=>l.ReportTableJoinParameterList)
			.Include(l=>l.ReportColumnDetailList)
			.Include(l=>l.ReportColumnFilterList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
