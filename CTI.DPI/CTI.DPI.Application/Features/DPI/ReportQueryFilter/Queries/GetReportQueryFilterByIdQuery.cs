using CTI.Common.Core.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportQueryFilter.Queries;

public record GetReportQueryFilterByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportQueryFilterState>>;

public class GetReportQueryFilterByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportQueryFilterState, GetReportQueryFilterByIdQuery>, IRequestHandler<GetReportQueryFilterByIdQuery, Option<ReportQueryFilterState>>
{
    public GetReportQueryFilterByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportQueryFilterState>> Handle(GetReportQueryFilterByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportQueryFilter.Include(l=>l.Report)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
