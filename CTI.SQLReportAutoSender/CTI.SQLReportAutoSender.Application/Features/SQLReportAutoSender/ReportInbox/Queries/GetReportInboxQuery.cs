using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Queries;

public record GetReportInboxQuery : BaseQuery, IRequest<PagedListResponse<ReportInboxState>>;

public class GetReportInboxQueryHandler : BaseQueryHandler<ApplicationContext, ReportInboxState, GetReportInboxQuery>, IRequestHandler<GetReportInboxQuery, PagedListResponse<ReportInboxState>>
{
    public GetReportInboxQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportInboxState>> Handle(GetReportInboxQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportInboxState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
