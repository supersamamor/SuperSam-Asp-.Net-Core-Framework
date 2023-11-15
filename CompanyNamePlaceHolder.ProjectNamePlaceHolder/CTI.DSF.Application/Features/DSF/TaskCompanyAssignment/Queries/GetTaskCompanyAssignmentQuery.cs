using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Queries;

public record GetTaskCompanyAssignmentQuery : BaseQuery, IRequest<PagedListResponse<TaskCompanyAssignmentState>>;

public class GetTaskCompanyAssignmentQueryHandler : BaseQueryHandler<ApplicationContext, TaskCompanyAssignmentState, GetTaskCompanyAssignmentQuery>, IRequestHandler<GetTaskCompanyAssignmentQuery, PagedListResponse<TaskCompanyAssignmentState>>
{
    public GetTaskCompanyAssignmentQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TaskCompanyAssignmentState>> Handle(GetTaskCompanyAssignmentQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TaskCompanyAssignmentState>().Include(l=>l.Team).Include(l=>l.Company).Include(l=>l.Department).Include(l=>l.Section).Include(l=>l.TaskMaster)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
