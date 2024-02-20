using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Department.Queries;

public record GetDepartmentQuery : BaseQuery, IRequest<PagedListResponse<DepartmentState>>;

public class GetDepartmentQueryHandler : BaseQueryHandler<ApplicationContext, DepartmentState, GetDepartmentQuery>, IRequestHandler<GetDepartmentQuery, PagedListResponse<DepartmentState>>
{
    public GetDepartmentQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<DepartmentState>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<DepartmentState>().Include(l=>l.Company)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
