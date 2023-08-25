using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskApprover.Queries;

public record GetTaskApproverQuery : BaseQuery, IRequest<PagedListResponse<TaskApproverState>>;

public class GetTaskApproverQueryHandler : BaseQueryHandler<ApplicationContext, TaskApproverState, GetTaskApproverQuery>, IRequestHandler<GetTaskApproverQuery, PagedListResponse<TaskApproverState>>
{
    public GetTaskApproverQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TaskApproverState>> Handle(GetTaskApproverQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TaskApproverState>().Include(l=>l.TaskList)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
