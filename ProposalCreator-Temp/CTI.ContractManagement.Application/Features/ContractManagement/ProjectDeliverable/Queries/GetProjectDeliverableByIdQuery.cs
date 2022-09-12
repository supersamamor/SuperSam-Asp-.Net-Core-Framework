using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Queries;

public record GetProjectDeliverableByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectDeliverableState>>;

public class GetProjectDeliverableByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectDeliverableState, GetProjectDeliverableByIdQuery>, IRequestHandler<GetProjectDeliverableByIdQuery, Option<ProjectDeliverableState>>
{
    public GetProjectDeliverableByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectDeliverableState>> Handle(GetProjectDeliverableByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectDeliverable.Include(l=>l.Project).Include(l=>l.Deliverable)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
