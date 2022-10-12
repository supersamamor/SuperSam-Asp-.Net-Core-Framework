using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Queries;

public record GetScheduleFrequencyParameterSetupQuery : BaseQuery, IRequest<PagedListResponse<ScheduleFrequencyParameterSetupState>>;

public class GetScheduleFrequencyParameterSetupQueryHandler : BaseQueryHandler<ApplicationContext, ScheduleFrequencyParameterSetupState, GetScheduleFrequencyParameterSetupQuery>, IRequestHandler<GetScheduleFrequencyParameterSetupQuery, PagedListResponse<ScheduleFrequencyParameterSetupState>>
{
    public GetScheduleFrequencyParameterSetupQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ScheduleFrequencyParameterSetupState>> Handle(GetScheduleFrequencyParameterSetupQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ScheduleFrequencyParameterSetupState>().Include(l=>l.ScheduleParameter).Include(l=>l.ScheduleFrequency)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
