using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Queries;

public record GetCustomScheduleQuery : BaseQuery, IRequest<PagedListResponse<CustomScheduleState>>;

public class GetCustomScheduleQueryHandler : BaseQueryHandler<ApplicationContext, CustomScheduleState, GetCustomScheduleQuery>, IRequestHandler<GetCustomScheduleQuery, PagedListResponse<CustomScheduleState>>
{
    public GetCustomScheduleQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CustomScheduleState>> Handle(GetCustomScheduleQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CustomScheduleState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
