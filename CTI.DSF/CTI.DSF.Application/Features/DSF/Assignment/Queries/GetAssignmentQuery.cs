using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Assignment.Queries;

public record GetAssignmentQuery : BaseQuery, IRequest<PagedListResponse<AssignmentState>>;

public class GetAssignmentQueryHandler : BaseQueryHandler<ApplicationContext, AssignmentState, GetAssignmentQuery>, IRequestHandler<GetAssignmentQuery, PagedListResponse<AssignmentState>>
{
    public GetAssignmentQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<AssignmentState>> Handle(GetAssignmentQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<AssignmentState>().Include(l=>l.TaskList)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
