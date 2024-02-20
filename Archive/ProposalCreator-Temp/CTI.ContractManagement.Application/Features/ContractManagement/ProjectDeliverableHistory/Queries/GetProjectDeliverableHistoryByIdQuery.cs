using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Queries;

public record GetProjectDeliverableHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectDeliverableHistoryState>>;

public class GetProjectDeliverableHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectDeliverableHistoryState, GetProjectDeliverableHistoryByIdQuery>, IRequestHandler<GetProjectDeliverableHistoryByIdQuery, Option<ProjectDeliverableHistoryState>>
{
    public GetProjectDeliverableHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectDeliverableHistoryState>> Handle(GetProjectDeliverableHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectDeliverableHistory.Include(l=>l.ProjectHistory).Include(l=>l.Deliverable)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
