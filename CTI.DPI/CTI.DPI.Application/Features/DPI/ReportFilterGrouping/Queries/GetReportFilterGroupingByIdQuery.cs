using CTI.Common.Core.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Queries;

public record GetReportFilterGroupingByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportFilterGroupingState>>;

public class GetReportFilterGroupingByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportFilterGroupingState, GetReportFilterGroupingByIdQuery>, IRequestHandler<GetReportFilterGroupingByIdQuery, Option<ReportFilterGroupingState>>
{
    public GetReportFilterGroupingByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportFilterGroupingState>> Handle(GetReportFilterGroupingByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportFilterGrouping.Include(l=>l.Report)
			.Include(l=>l.ReportColumnFilterList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
