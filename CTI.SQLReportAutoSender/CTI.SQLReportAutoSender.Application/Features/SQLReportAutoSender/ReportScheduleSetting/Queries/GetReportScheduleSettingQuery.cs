using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Queries;

public record GetReportScheduleSettingQuery : BaseQuery, IRequest<PagedListResponse<ReportScheduleSettingState>>;

public class GetReportScheduleSettingQueryHandler : BaseQueryHandler<ApplicationContext, ReportScheduleSettingState, GetReportScheduleSettingQuery>, IRequestHandler<GetReportScheduleSettingQuery, PagedListResponse<ReportScheduleSettingState>>
{
    public GetReportScheduleSettingQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportScheduleSettingState>> Handle(GetReportScheduleSettingQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportScheduleSettingState>().Include(l=>l.ScheduleFrequency).Include(l=>l.Report).Include(l=>l.ScheduleParameter)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
