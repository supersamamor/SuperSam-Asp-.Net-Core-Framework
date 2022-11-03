using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Queries;

public record GetUserProjectAssignmentQuery : BaseQuery, IRequest<PagedListResponse<UserProjectAssignmentState>>;

public class GetUserProjectAssignmentQueryHandler : BaseQueryHandler<ApplicationContext, UserProjectAssignmentState, GetUserProjectAssignmentQuery>, IRequestHandler<GetUserProjectAssignmentQuery, PagedListResponse<UserProjectAssignmentState>>
{
    public GetUserProjectAssignmentQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UserProjectAssignmentState>> Handle(GetUserProjectAssignmentQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UserProjectAssignmentState>().Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
