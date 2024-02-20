using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Queries;

public record GetProjectHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectHistoryState>>;

public class GetProjectHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectHistoryState, GetProjectHistoryByIdQuery>, IRequestHandler<GetProjectHistoryByIdQuery, Option<ProjectHistoryState>>
{
    public GetProjectHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectHistoryState>> Handle(GetProjectHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectHistory.Include(l=>l.Project).Include(l=>l.Client).Include(l=>l.PricingType)
			.Include(l=>l.ProjectDeliverableHistoryList)
			.Include(l=>l.ProjectMilestoneHistoryList)
			.Include(l=>l.ProjectPackageHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
