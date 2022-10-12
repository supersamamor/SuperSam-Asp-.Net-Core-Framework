using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Queries;

public record GetScheduleFrequencyQuery : BaseQuery, IRequest<PagedListResponse<ScheduleFrequencyState>>;

public class GetScheduleFrequencyQueryHandler : BaseQueryHandler<ApplicationContext, ScheduleFrequencyState, GetScheduleFrequencyQuery>, IRequestHandler<GetScheduleFrequencyQuery, PagedListResponse<ScheduleFrequencyState>>
{
    public GetScheduleFrequencyQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
