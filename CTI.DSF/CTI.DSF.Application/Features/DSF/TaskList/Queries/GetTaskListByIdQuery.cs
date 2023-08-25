using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskList.Queries;

public record GetTaskListByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TaskListState>>;

public class GetTaskListByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TaskListState, GetTaskListByIdQuery>, IRequestHandler<GetTaskListByIdQuery, Option<TaskListState>>
{
    public GetTaskListByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TaskListState>> Handle(GetTaskListByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TaskList.Include(l=>l.Department).Include(l=>l.Team).Include(l=>l.Section).Include(l=>l.Company)
			.Include(l=>l.TaskApproverList)
			.Include(l=>l.TaskTagList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
