using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportColumnFilter.Queries;

public record GetReportColumnFilterQuery : BaseQuery, IRequest<PagedListResponse<ReportColumnFilterState>>;

public class GetReportColumnFilterQueryHandler : BaseQueryHandler<ApplicationContext, ReportColumnFilterState, GetReportColumnFilterQuery>, IRequestHandler<GetReportColumnFilterQuery, PagedListResponse<ReportColumnFilterState>>
{
    public GetReportColumnFilterQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportColumnFilterState>> Handle(GetReportColumnFilterQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportColumnFilterState>().Include(l=>l.ReportTable).Include(l=>l.ReportFilterGrouping)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
