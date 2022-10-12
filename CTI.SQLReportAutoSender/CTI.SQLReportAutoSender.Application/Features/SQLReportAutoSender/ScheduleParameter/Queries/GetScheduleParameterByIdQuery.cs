using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Queries;

public record GetScheduleParameterByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ScheduleParameterState>>;

public class GetScheduleParameterByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ScheduleParameterState, GetScheduleParameterByIdQuery>, IRequestHandler<GetScheduleParameterByIdQuery, Option<ScheduleParameterState>>
{
    public GetScheduleParameterByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ScheduleParameterState>> Handle(GetScheduleParameterByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ScheduleParameter
			.Include(l=>l.ScheduleFrequencyParameterSetupList)
			.Include(l=>l.ReportScheduleSettingList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
