using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Queries;

public record GetProjectPackageAdditionalDeliverableHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectPackageAdditionalDeliverableHistoryState>>;

public class GetProjectPackageAdditionalDeliverableHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectPackageAdditionalDeliverableHistoryState, GetProjectPackageAdditionalDeliverableHistoryByIdQuery>, IRequestHandler<GetProjectPackageAdditionalDeliverableHistoryByIdQuery, Option<ProjectPackageAdditionalDeliverableHistoryState>>
{
    public GetProjectPackageAdditionalDeliverableHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectPackageAdditionalDeliverableHistoryState>> Handle(GetProjectPackageAdditionalDeliverableHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectPackageAdditionalDeliverableHistory.Include(l=>l.ProjectPackageHistory).Include(l=>l.Deliverable)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
