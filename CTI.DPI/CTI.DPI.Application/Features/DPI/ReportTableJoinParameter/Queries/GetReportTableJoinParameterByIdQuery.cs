using CTI.Common.Core.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Queries;

public record GetReportTableJoinParameterByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportTableJoinParameterState>>;

public class GetReportTableJoinParameterByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportTableJoinParameterState, GetReportTableJoinParameterByIdQuery>, IRequestHandler<GetReportTableJoinParameterByIdQuery, Option<ReportTableJoinParameterState>>
{
    public GetReportTableJoinParameterByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportTableJoinParameterState>> Handle(GetReportTableJoinParameterByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportTableJoinParameter.Include(l=>l.ReportTable).Include(l=>l.Report)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
