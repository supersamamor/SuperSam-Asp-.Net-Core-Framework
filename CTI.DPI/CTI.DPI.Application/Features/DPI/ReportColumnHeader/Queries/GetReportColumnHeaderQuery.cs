using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportColumnHeader.Queries;

public record GetReportColumnHeaderQuery : BaseQuery, IRequest<PagedListResponse<ReportColumnHeaderState>>;

public class GetReportColumnHeaderQueryHandler : BaseQueryHandler<ApplicationContext, ReportColumnHeaderState, GetReportColumnHeaderQuery>, IRequestHandler<GetReportColumnHeaderQuery, PagedListResponse<ReportColumnHeaderState>>
{
    public GetReportColumnHeaderQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportColumnHeaderState>> Handle(GetReportColumnHeaderQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportColumnHeaderState>().Include(l=>l.Report)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
