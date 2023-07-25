using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Queries;

public record GetReportFilterGroupingQuery : BaseQuery, IRequest<PagedListResponse<ReportFilterGroupingState>>;

public class GetReportFilterGroupingQueryHandler : BaseQueryHandler<ApplicationContext, ReportFilterGroupingState, GetReportFilterGroupingQuery>, IRequestHandler<GetReportFilterGroupingQuery, PagedListResponse<ReportFilterGroupingState>>
{
    public GetReportFilterGroupingQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportFilterGroupingState>> Handle(GetReportFilterGroupingQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportFilterGroupingState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
