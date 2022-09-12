using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Queries;

public record GetProjectPackageAdditionalDeliverableHistoryQuery : BaseQuery, IRequest<PagedListResponse<ProjectPackageAdditionalDeliverableHistoryState>>;

public class GetProjectPackageAdditionalDeliverableHistoryQueryHandler : BaseQueryHandler<ApplicationContext, ProjectPackageAdditionalDeliverableHistoryState, GetProjectPackageAdditionalDeliverableHistoryQuery>, IRequestHandler<GetProjectPackageAdditionalDeliverableHistoryQuery, PagedListResponse<ProjectPackageAdditionalDeliverableHistoryState>>
{
    public GetProjectPackageAdditionalDeliverableHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectPackageAdditionalDeliverableHistoryState>> Handle(GetProjectPackageAdditionalDeliverableHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectPackageAdditionalDeliverableHistoryState>().Include(l=>l.ProjectPackageHistory).Include(l=>l.Deliverable)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
