using CTI.Common.Core.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportColumnFilter.Queries;

public record GetReportColumnFilterByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportColumnFilterState>>;

public class GetReportColumnFilterByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportColumnFilterState, GetReportColumnFilterByIdQuery>, IRequestHandler<GetReportColumnFilterByIdQuery, Option<ReportColumnFilterState>>
{
    public GetReportColumnFilterByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportColumnFilterState>> Handle(GetReportColumnFilterByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportColumnFilter.Include(l=>l.ReportTable).Include(l=>l.ReportFilterGrouping)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
