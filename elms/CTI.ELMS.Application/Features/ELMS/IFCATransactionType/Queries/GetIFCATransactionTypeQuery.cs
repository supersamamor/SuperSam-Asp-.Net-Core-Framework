using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Queries;

public record GetIFCATransactionTypeQuery : BaseQuery, IRequest<PagedListResponse<IFCATransactionTypeState>>;

public class GetIFCATransactionTypeQueryHandler : BaseQueryHandler<ApplicationContext, IFCATransactionTypeState, GetIFCATransactionTypeQuery>, IRequestHandler<GetIFCATransactionTypeQuery, PagedListResponse<IFCATransactionTypeState>>
{
    public GetIFCATransactionTypeQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<IFCATransactionTypeState>> Handle(GetIFCATransactionTypeQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<IFCATransactionTypeState>().Include(l=>l.EntityGroup)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
