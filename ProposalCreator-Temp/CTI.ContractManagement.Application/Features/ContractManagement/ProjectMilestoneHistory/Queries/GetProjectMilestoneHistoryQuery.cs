using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Queries;

public record GetProjectMilestoneHistoryQuery : BaseQuery, IRequest<PagedListResponse<ProjectMilestoneHistoryState>>;

public class GetProjectMilestoneHistoryQueryHandler : BaseQueryHandler<ApplicationContext, ProjectMilestoneHistoryState, GetProjectMilestoneHistoryQuery>, IRequestHandler<GetProjectMilestoneHistoryQuery, PagedListResponse<ProjectMilestoneHistoryState>>
{
    public GetProjectMilestoneHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectMilestoneHistoryState>> Handle(GetProjectMilestoneHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectMilestoneHistoryState>().Include(l=>l.ProjectHistory).Include(l=>l.MilestoneStage).Include(l=>l.Frequency)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
