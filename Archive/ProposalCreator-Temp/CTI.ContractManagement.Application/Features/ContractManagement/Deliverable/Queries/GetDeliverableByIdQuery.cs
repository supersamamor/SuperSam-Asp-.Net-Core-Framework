using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Queries;

public record GetDeliverableByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<DeliverableState>>;

public class GetDeliverableByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, DeliverableState, GetDeliverableByIdQuery>, IRequestHandler<GetDeliverableByIdQuery, Option<DeliverableState>>
{
    public GetDeliverableByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<DeliverableState>> Handle(GetDeliverableByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Deliverable.Include(l=>l.ProjectCategory)
			.Include(l=>l.ProjectDeliverableList)
			.Include(l=>l.ProjectPackageAdditionalDeliverableList)
			.Include(l=>l.ProjectDeliverableHistoryList)
			.Include(l=>l.ProjectPackageAdditionalDeliverableHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
