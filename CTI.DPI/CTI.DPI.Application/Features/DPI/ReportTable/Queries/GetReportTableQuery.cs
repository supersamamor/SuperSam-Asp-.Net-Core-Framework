using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportTable.Queries;

public record GetReportTableQuery : BaseQuery, IRequest<PagedListResponse<ReportTableState>>;

public class GetReportTableQueryHandler : BaseQueryHandler<ApplicationContext, ReportTableState, GetReportTableQuery>, IRequestHandler<GetReportTableQuery, PagedListResponse<ReportTableState>>
{
    public GetReportTableQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportTableState>> Handle(GetReportTableQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportTableState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
