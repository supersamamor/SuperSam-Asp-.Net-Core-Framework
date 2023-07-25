using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Queries;

public record GetReportTableJoinParameterQuery : BaseQuery, IRequest<PagedListResponse<ReportTableJoinParameterState>>;

public class GetReportTableJoinParameterQueryHandler : BaseQueryHandler<ApplicationContext, ReportTableJoinParameterState, GetReportTableJoinParameterQuery>, IRequestHandler<GetReportTableJoinParameterQuery, PagedListResponse<ReportTableJoinParameterState>>
{
    public GetReportTableJoinParameterQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportTableJoinParameterState>> Handle(GetReportTableJoinParameterQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportTableJoinParameterState>().Include(l=>l.ReportTable).Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
