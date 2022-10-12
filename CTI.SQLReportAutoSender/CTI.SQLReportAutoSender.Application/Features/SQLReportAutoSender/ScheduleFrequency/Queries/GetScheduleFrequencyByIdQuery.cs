using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Queries;

public record GetScheduleFrequencyByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ScheduleFrequencyState>>;

public class GetScheduleFrequencyByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ScheduleFrequencyState, GetScheduleFrequencyByIdQuery>, IRequestHandler<GetScheduleFrequencyByIdQuery, Option<ScheduleFrequencyState>>
{
    public GetScheduleFrequencyByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ScheduleFrequencyState>> Handle(GetScheduleFrequencyByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ScheduleFrequency
			.Include(l=>l.ScheduleFrequencyParameterSetupList)
			.Include(l=>l.ReportScheduleSettingList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
