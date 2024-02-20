using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Queries;

public record GetProjectHistoryQuery : BaseQuery, IRequest<PagedListResponse<ProjectHistoryState>>;

public class GetProjectHistoryQueryHandler : BaseQueryHandler<ApplicationContext, ProjectHistoryState, GetProjectHistoryQuery>, IRequestHandler<GetProjectHistoryQuery, PagedListResponse<ProjectHistoryState>>
{
    public GetProjectHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProjectHistoryState>> Handle(GetProjectHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProjectHistoryState>().Include(l=>l.Project).Include(l=>l.Client).Include(l=>l.PricingType)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
