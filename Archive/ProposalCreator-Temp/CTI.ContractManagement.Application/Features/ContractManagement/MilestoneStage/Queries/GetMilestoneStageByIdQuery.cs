using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Queries;

public record GetMilestoneStageByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<MilestoneStageState>>;

public class GetMilestoneStageByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, MilestoneStageState, GetMilestoneStageByIdQuery>, IRequestHandler<GetMilestoneStageByIdQuery, Option<MilestoneStageState>>
{
    public GetMilestoneStageByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<MilestoneStageState>> Handle(GetMilestoneStageByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.MilestoneStage
			.Include(l=>l.ProjectMilestoneList)
			.Include(l=>l.ProjectMilestoneHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
