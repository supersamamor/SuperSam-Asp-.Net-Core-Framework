using CTI.Common.Core.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportColumnDetail.Queries;

public record GetReportColumnDetailByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportColumnDetailState>>;

public class GetReportColumnDetailByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportColumnDetailState, GetReportColumnDetailByIdQuery>, IRequestHandler<GetReportColumnDetailByIdQuery, Option<ReportColumnDetailState>>
{
    public GetReportColumnDetailByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportColumnDetailState>> Handle(GetReportColumnDetailByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportColumnDetail.Include(l=>l.ReportTable).Include(l=>l.ReportColumnHeader)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
