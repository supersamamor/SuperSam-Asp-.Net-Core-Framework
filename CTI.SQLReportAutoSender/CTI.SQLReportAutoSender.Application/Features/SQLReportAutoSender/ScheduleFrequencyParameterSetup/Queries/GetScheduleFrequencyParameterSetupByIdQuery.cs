using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Queries;

public record GetScheduleFrequencyParameterSetupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ScheduleFrequencyParameterSetupState>>;

public class GetScheduleFrequencyParameterSetupByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ScheduleFrequencyParameterSetupState, GetScheduleFrequencyParameterSetupByIdQuery>, IRequestHandler<GetScheduleFrequencyParameterSetupByIdQuery, Option<ScheduleFrequencyParameterSetupState>>
{
    public GetScheduleFrequencyParameterSetupByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ScheduleFrequencyParameterSetupState>> Handle(GetScheduleFrequencyParameterSetupByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ScheduleFrequencyParameterSetup.Include(l=>l.ScheduleParameter).Include(l=>l.ScheduleFrequency)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
