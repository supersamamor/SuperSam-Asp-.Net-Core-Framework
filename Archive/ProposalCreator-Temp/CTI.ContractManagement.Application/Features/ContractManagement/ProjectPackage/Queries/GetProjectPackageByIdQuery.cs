using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Queries;

public record GetProjectPackageByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectPackageState>>;

public class GetProjectPackageByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectPackageState, GetProjectPackageByIdQuery>, IRequestHandler<GetProjectPackageByIdQuery, Option<ProjectPackageState>>
{
    public GetProjectPackageByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectPackageState>> Handle(GetProjectPackageByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectPackage.Include(l=>l.Project)
			.Include(l=>l.ProjectPackageAdditionalDeliverableList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
