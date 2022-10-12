using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Queries;

public record GetReportByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportState>>;

public class GetReportByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportState, GetReportByIdQuery>, IRequestHandler<GetReportByIdQuery, Option<ReportState>>
{
    public GetReportByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportState>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Report.Include(l=>l.ScheduleFrequency)
			.Include(l=>l.ReportDetailList)
			.Include(l=>l.MailSettingList)
			.Include(l=>l.MailRecipientList)
			.Include(l=>l.ReportScheduleSettingList)
			.Include(l=>l.CustomScheduleList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
