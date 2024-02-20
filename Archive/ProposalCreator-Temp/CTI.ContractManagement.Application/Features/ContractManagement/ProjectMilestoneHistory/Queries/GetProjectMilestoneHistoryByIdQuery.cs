using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Queries;

public record GetProjectMilestoneHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectMilestoneHistoryState>>;

public class GetProjectMilestoneHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectMilestoneHistoryState, GetProjectMilestoneHistoryByIdQuery>, IRequestHandler<GetProjectMilestoneHistoryByIdQuery, Option<ProjectMilestoneHistoryState>>
{
    public GetProjectMilestoneHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectMilestoneHistoryState>> Handle(GetProjectMilestoneHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectMilestoneHistory.Include(l=>l.ProjectHistory).Include(l=>l.MilestoneStage).Include(l=>l.Frequency)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
