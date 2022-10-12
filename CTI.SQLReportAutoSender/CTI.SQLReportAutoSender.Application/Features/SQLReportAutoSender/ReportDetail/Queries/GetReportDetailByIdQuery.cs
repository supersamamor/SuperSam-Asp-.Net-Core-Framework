using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportDetail.Queries;

public record GetReportDetailByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportDetailState>>;

public class GetReportDetailByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportDetailState, GetReportDetailByIdQuery>, IRequestHandler<GetReportDetailByIdQuery, Option<ReportDetailState>>
{
    public GetReportDetailByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportDetailState>> Handle(GetReportDetailByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportDetail.Include(l=>l.Report)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
