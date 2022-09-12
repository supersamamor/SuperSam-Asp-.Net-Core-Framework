using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Queries;

public record GetFrequencyByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<FrequencyState>>;

public class GetFrequencyByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, FrequencyState, GetFrequencyByIdQuery>, IRequestHandler<GetFrequencyByIdQuery, Option<FrequencyState>>
{
    public GetFrequencyByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<FrequencyState>> Handle(GetFrequencyByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Frequency
			.Include(l=>l.ProjectMilestoneList)
			.Include(l=>l.ProjectMilestoneHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
