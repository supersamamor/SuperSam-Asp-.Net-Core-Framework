using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Queries;

public record GetProjectPackageHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectPackageHistoryState>>;

public class GetProjectPackageHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectPackageHistoryState, GetProjectPackageHistoryByIdQuery>, IRequestHandler<GetProjectPackageHistoryByIdQuery, Option<ProjectPackageHistoryState>>
{
    public GetProjectPackageHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectPackageHistoryState>> Handle(GetProjectPackageHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProjectPackageHistory.Include(l=>l.ProjectHistory)
			.Include(l=>l.ProjectPackageAdditionalDeliverableHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
