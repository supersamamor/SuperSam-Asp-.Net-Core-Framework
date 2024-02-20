using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Queries;

public record GetProjectPackageQuery : BaseQuery, IRequest<PagedListResponse<ProjectPackageState>>;

public class GetProjectPackageQueryHandler : BaseQueryHandler<ApplicationContext, ProjectPackageState, GetProjectPackageQuery>, IRequestHandler<GetProjectPackageQuery, PagedListResponse<ProjectPackageState>>
{
    public GetProjectPackageQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectPackageState>> Handle(GetProjectPackageQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectPackageState>().Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
