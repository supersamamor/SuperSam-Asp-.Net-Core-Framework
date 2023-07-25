using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportQueryFilter.Queries;

public record GetReportQueryFilterQuery : BaseQuery, IRequest<PagedListResponse<ReportQueryFilterState>>;

public class GetReportQueryFilterQueryHandler : BaseQueryHandler<ApplicationContext, ReportQueryFilterState, GetReportQueryFilterQuery>, IRequestHandler<GetReportQueryFilterQuery, PagedListResponse<ReportQueryFilterState>>
{
    public GetReportQueryFilterQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportQueryFilterState>> Handle(GetReportQueryFilterQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportQueryFilterState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
