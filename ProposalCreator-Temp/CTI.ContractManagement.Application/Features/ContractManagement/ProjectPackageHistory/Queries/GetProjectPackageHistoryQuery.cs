using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Queries;

public record GetProjectPackageHistoryQuery : BaseQuery, IRequest<PagedListResponse<ProjectPackageHistoryState>>;

public class GetProjectPackageHistoryQueryHandler : BaseQueryHandler<ApplicationContext, ProjectPackageHistoryState, GetProjectPackageHistoryQuery>, IRequestHandler<GetProjectPackageHistoryQuery, PagedListResponse<ProjectPackageHistoryState>>
{
    public GetProjectPackageHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectPackageHistoryState>> Handle(GetProjectPackageHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectPackageHistoryState>().Include(l=>l.ProjectHistory)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
