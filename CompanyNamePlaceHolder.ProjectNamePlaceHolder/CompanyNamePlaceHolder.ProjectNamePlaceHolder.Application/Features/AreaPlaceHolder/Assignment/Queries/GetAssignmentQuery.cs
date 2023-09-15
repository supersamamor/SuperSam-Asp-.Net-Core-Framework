using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Assignment.Queries;

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
