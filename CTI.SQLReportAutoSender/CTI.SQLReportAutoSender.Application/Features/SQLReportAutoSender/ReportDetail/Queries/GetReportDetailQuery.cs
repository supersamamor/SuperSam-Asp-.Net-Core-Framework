using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportDetail.Queries;

public record GetReportDetailQuery : BaseQuery, IRequest<PagedListResponse<ReportDetailState>>;

public class GetReportDetailQueryHandler : BaseQueryHandler<ApplicationContext, ReportDetailState, GetReportDetailQuery>, IRequestHandler<GetReportDetailQuery, PagedListResponse<ReportDetailState>>
{
    public GetReportDetailQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportDetailState>> Handle(GetReportDetailQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportDetailState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
