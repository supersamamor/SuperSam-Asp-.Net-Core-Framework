using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverable.Queries;

public record GetProjectPackageAdditionalDeliverableQuery : BaseQuery, IRequest<PagedListResponse<ProjectPackageAdditionalDeliverableState>>;

public class GetProjectPackageAdditionalDeliverableQueryHandler : BaseQueryHandler<ApplicationContext, ProjectPackageAdditionalDeliverableState, GetProjectPackageAdditionalDeliverableQuery>, IRequestHandler<GetProjectPackageAdditionalDeliverableQuery, PagedListResponse<ProjectPackageAdditionalDeliverableState>>
{
    public GetProjectPackageAdditionalDeliverableQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectPackageAdditionalDeliverableState>> Handle(GetProjectPackageAdditionalDeliverableQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectPackageAdditionalDeliverableState>().Include(l=>l.ProjectPackage).Include(l=>l.Deliverable)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
