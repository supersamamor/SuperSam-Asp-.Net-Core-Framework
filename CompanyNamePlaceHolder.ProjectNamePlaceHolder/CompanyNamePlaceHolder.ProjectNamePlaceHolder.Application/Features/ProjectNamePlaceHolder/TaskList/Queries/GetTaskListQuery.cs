using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.TaskList.Queries;

public record GetTaskListQuery : BaseQuery, IRequest<PagedListResponse<TaskListState>>;

public class GetTaskListQueryHandler : BaseQueryHandler<ApplicationContext, TaskListState, GetTaskListQuery>, IRequestHandler<GetTaskListQuery, PagedListResponse<TaskListState>>
{
    public GetTaskListQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
