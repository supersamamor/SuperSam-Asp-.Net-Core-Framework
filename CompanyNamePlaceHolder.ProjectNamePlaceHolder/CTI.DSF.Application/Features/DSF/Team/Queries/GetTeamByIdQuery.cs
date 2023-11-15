using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Team.Queries;

public record GetTeamByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TeamState>>;

public class GetTeamByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TeamState, GetTeamByIdQuery>, IRequestHandler<GetTeamByIdQuery, Option<TeamState>>
{
    public GetTeamByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TeamState>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Team.Include(l=>l.Section)
			.Include(l=>l.TaskCompanyAssignmentList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
