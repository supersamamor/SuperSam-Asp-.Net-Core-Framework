using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskTag.Queries;

public record GetTaskTagQuery : BaseQuery, IRequest<PagedListResponse<TaskTagState>>;

public class GetTaskTagQueryHandler : BaseQueryHandler<ApplicationContext, TaskTagState, GetTaskTagQuery>, IRequestHandler<GetTaskTagQuery, PagedListResponse<TaskTagState>>
{
    public GetTaskTagQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TaskTagState>> Handle(GetTaskTagQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TaskTagState>().Include(l=>l.TaskList).Include(l=>l.Tags)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
