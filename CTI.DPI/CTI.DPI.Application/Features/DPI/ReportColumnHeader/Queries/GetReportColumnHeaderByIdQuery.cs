using CTI.Common.Core.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportColumnHeader.Queries;

public record GetReportColumnHeaderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportColumnHeaderState>>;

public class GetReportColumnHeaderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportColumnHeaderState, GetReportColumnHeaderByIdQuery>, IRequestHandler<GetReportColumnHeaderByIdQuery, Option<ReportColumnHeaderState>>
{
    public GetReportColumnHeaderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportColumnHeaderState>> Handle(GetReportColumnHeaderByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportColumnHeader.Include(l=>l.Report)
			.Include(l=>l.ReportColumnDetailList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
