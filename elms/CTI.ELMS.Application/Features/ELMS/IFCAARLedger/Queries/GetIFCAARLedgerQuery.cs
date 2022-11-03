using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Queries;

public record GetIFCAARLedgerQuery : BaseQuery, IRequest<PagedListResponse<IFCAARLedgerState>>;

public class GetIFCAARLedgerQueryHandler : BaseQueryHandler<ApplicationContext, IFCAARLedgerState, GetIFCAARLedgerQuery>, IRequestHandler<GetIFCAARLedgerQuery, PagedListResponse<IFCAARLedgerState>>
{
    public GetIFCAARLedgerQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<IFCAARLedgerState>> Handle(GetIFCAARLedgerQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<IFCAARLedgerState>().Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
