using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskApprover.Queries;

public record GetTaskApproverByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TaskApproverState>>;

public class GetTaskApproverByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TaskApproverState, GetTaskApproverByIdQuery>, IRequestHandler<GetTaskApproverByIdQuery, Option<TaskApproverState>>
{
    public GetTaskApproverByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TaskApproverState>> Handle(GetTaskApproverByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TaskApprover.Include(l=>l.TaskList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
