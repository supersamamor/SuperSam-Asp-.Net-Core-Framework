using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Queries;

public record GetReportInboxByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportInboxState>>;

public class GetReportInboxByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportInboxState, GetReportInboxByIdQuery>, IRequestHandler<GetReportInboxByIdQuery, Option<ReportInboxState>>
{
    public GetReportInboxByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportInboxState>> Handle(GetReportInboxByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportInbox.Include(l=>l.Report)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
