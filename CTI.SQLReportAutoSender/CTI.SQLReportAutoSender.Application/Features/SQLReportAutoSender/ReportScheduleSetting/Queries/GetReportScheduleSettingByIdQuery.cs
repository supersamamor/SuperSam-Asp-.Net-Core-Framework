using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Queries;

public record GetReportScheduleSettingByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportScheduleSettingState>>;

public class GetReportScheduleSettingByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportScheduleSettingState, GetReportScheduleSettingByIdQuery>, IRequestHandler<GetReportScheduleSettingByIdQuery, Option<ReportScheduleSettingState>>
{
    public GetReportScheduleSettingByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ReportScheduleSettingState>> Handle(GetReportScheduleSettingByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ReportScheduleSetting.Include(l=>l.ScheduleFrequency).Include(l=>l.Report).Include(l=>l.ScheduleParameter)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
