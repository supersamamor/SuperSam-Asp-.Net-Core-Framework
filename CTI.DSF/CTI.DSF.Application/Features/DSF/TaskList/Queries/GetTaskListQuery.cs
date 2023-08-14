using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskList.Queries;

public record GetTaskListQuery : BaseQuery, IRequest<PagedListResponse<TaskListState>>;

public class GetTaskListQueryHandler : BaseQueryHandler<ApplicationContext, TaskListState, GetTaskListQuery>, IRequestHandler<GetTaskListQuery, PagedListResponse<TaskListState>>
{
    public GetTaskListQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<TaskListState>> Handle(GetTaskListQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.TaskList.Where(l=>l.IsMilestone != true).AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                 request.SortColumn, request.SortOrder,
                                                                 request.PageNumber, request.PageSize,
                                                                 cancellationToken);
    }

}
