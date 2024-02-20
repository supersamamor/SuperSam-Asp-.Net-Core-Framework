using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Queries;

public record GetProjectDeliverableHistoryQuery : BaseQuery, IRequest<PagedListResponse<ProjectDeliverableHistoryState>>;

public class GetProjectDeliverableHistoryQueryHandler : BaseQueryHandler<ApplicationContext, ProjectDeliverableHistoryState, GetProjectDeliverableHistoryQuery>, IRequestHandler<GetProjectDeliverableHistoryQuery, PagedListResponse<ProjectDeliverableHistoryState>>
{
    public GetProjectDeliverableHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectDeliverableHistoryState>> Handle(GetProjectDeliverableHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectDeliverableHistoryState>().Include(l=>l.ProjectHistory).Include(l=>l.Deliverable)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
