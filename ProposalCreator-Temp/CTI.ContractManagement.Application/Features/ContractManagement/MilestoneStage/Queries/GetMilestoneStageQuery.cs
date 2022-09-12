using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Queries;

public record GetMilestoneStageQuery : BaseQuery, IRequest<PagedListResponse<MilestoneStageState>>;

public class GetMilestoneStageQueryHandler : BaseQueryHandler<ApplicationContext, MilestoneStageState, GetMilestoneStageQuery>, IRequestHandler<GetMilestoneStageQuery, PagedListResponse<MilestoneStageState>>
{
    public GetMilestoneStageQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
