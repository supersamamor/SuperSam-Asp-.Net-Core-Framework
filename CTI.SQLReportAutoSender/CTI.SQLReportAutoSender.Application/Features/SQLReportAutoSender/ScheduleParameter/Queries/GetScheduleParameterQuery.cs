using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Queries;

public record GetScheduleParameterQuery : BaseQuery, IRequest<PagedListResponse<ScheduleParameterState>>;

public class GetScheduleParameterQueryHandler : BaseQueryHandler<ApplicationContext, ScheduleParameterState, GetScheduleParameterQuery>, IRequestHandler<GetScheduleParameterQuery, PagedListResponse<ScheduleParameterState>>
{
    public GetScheduleParameterQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
