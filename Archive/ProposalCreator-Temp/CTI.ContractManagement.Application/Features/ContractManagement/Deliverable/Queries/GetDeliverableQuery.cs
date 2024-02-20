using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Queries;

public record GetDeliverableQuery : BaseQuery, IRequest<PagedListResponse<DeliverableState>>;

public class GetDeliverableQueryHandler : BaseQueryHandler<ApplicationContext, DeliverableState, GetDeliverableQuery>, IRequestHandler<GetDeliverableQuery, PagedListResponse<DeliverableState>>
{
    public GetDeliverableQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<DeliverableState>> Handle(GetDeliverableQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<DeliverableState>().Include(l=>l.ProjectCategory)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
