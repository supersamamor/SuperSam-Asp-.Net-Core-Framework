using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.TaskList.Queries;

public record GetTaskListByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TaskListState>>;

public class GetTaskListByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TaskListState, GetTaskListByIdQuery>, IRequestHandler<GetTaskListByIdQuery, Option<TaskListState>>
{
    public GetTaskListByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TaskListState>> Handle(GetTaskListByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TaskList
			.Include(l=>l.AssignmentList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
