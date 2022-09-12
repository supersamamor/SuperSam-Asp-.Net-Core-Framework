using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Queries;

public record GetProjectDeliverableQuery : BaseQuery, IRequest<PagedListResponse<ProjectDeliverableState>>;

public class GetProjectDeliverableQueryHandler : BaseQueryHandler<ApplicationContext, ProjectDeliverableState, GetProjectDeliverableQuery>, IRequestHandler<GetProjectDeliverableQuery, PagedListResponse<ProjectDeliverableState>>
{
    public GetProjectDeliverableQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectDeliverableState>> Handle(GetProjectDeliverableQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectDeliverableState>().Include(l=>l.Project).Include(l=>l.Deliverable)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
