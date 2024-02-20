using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Team.Queries;

public record GetTeamQuery : BaseQuery, IRequest<PagedListResponse<TeamState>>;

public class GetTeamQueryHandler : BaseQueryHandler<ApplicationContext, TeamState, GetTeamQuery>, IRequestHandler<GetTeamQuery, PagedListResponse<TeamState>>
{
    public GetTeamQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TeamState>> Handle(GetTeamQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TeamState>().Include(l=>l.Section)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
