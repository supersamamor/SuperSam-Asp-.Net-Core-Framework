using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.ReportColumnDetail.Queries;

public record GetReportColumnDetailQuery : BaseQuery, IRequest<PagedListResponse<ReportColumnDetailState>>;

public class GetReportColumnDetailQueryHandler : BaseQueryHandler<ApplicationContext, ReportColumnDetailState, GetReportColumnDetailQuery>, IRequestHandler<GetReportColumnDetailQuery, PagedListResponse<ReportColumnDetailState>>
{
    public GetReportColumnDetailQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportColumnDetailState>> Handle(GetReportColumnDetailQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportColumnDetailState>().Include(l=>l.ReportTable).Include(l=>l.ReportColumnHeader)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
