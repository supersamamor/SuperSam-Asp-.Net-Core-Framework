using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverable.Queries;

public record GetProjectPackageAdditionalDeliverableByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectPackageAdditionalDeliverableState>>;

public class GetProjectPackageAdditionalDeliverableByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectPackageAdditionalDeliverableState, GetProjectPackageAdditionalDeliverableByIdQuery>, IRequestHandler<GetProjectPackageAdditionalDeliverableByIdQuery, Option<ProjectPackageAdditionalDeliverableState>>
{
    public GetProjectPackageAdditionalDeliverableByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectPackageAdditionalDeliverableState>> Handle(GetProjectPackageAdditionalDeliverableByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectPackageAdditionalDeliverable.Include(l=>l.ProjectPackage).Include(l=>l.Deliverable)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
