using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Queries;

public record GetProjectMilestoneByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectMilestoneState>>;

public class GetProjectMilestoneByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectMilestoneState, GetProjectMilestoneByIdQuery>, IRequestHandler<GetProjectMilestoneByIdQuery, Option<ProjectMilestoneState>>
{
    public GetProjectMilestoneByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectMilestoneState>> Handle(GetProjectMilestoneByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectMilestone.Include(l=>l.Frequency).Include(l=>l.MilestoneStage).Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
