using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.EntityGroup.Queries;

public record GetEntityGroupQuery : BaseQuery, IRequest<PagedListResponse<EntityGroupState>>;

public class GetEntityGroupQueryHandler : BaseQueryHandler<ApplicationContext, EntityGroupState, GetEntityGroupQuery>, IRequestHandler<GetEntityGroupQuery, PagedListResponse<EntityGroupState>>
{
    public GetEntityGroupQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<EntityGroupState>> Handle(GetEntityGroupQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<EntityGroupState>().Include(l=>l.PPlusConnectionSetup)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
