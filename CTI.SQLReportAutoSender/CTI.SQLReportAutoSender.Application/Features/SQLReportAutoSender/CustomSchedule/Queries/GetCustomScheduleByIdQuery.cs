using CTI.Common.Core.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Queries;

public record GetCustomScheduleByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CustomScheduleState>>;

public class GetCustomScheduleByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CustomScheduleState, GetCustomScheduleByIdQuery>, IRequestHandler<GetCustomScheduleByIdQuery, Option<CustomScheduleState>>
{
    public GetCustomScheduleByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CustomScheduleState>> Handle(GetCustomScheduleByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.CustomSchedule.Include(l=>l.Report)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
