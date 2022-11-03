using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Queries;

public record GetReportTableCollectionDetailQuery : BaseQuery, IRequest<PagedListResponse<ReportTableCollectionDetailState>>;

public class GetReportTableCollectionDetailQueryHandler : BaseQueryHandler<ApplicationContext, ReportTableCollectionDetailState, GetReportTableCollectionDetailQuery>, IRequestHandler<GetReportTableCollectionDetailQuery, PagedListResponse<ReportTableCollectionDetailState>>
{
    public GetReportTableCollectionDetailQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ReportTableCollectionDetailState>> Handle(GetReportTableCollectionDetailQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ReportTableCollectionDetailState>().Include(l=>l.Project).Include(l=>l.IFCATenantInformation)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
