using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Queries;

public record GetProjectMilestoneQuery : BaseQuery, IRequest<PagedListResponse<ProjectMilestoneState>>;

public class GetProjectMilestoneQueryHandler : BaseQueryHandler<ApplicationContext, ProjectMilestoneState, GetProjectMilestoneQuery>, IRequestHandler<GetProjectMilestoneQuery, PagedListResponse<ProjectMilestoneState>>
{
    public GetProjectMilestoneQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectMilestoneState>> Handle(GetProjectMilestoneQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectMilestoneState>().Include(l=>l.Frequency).Include(l=>l.MilestoneStage).Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
