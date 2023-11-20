using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskMaster.Queries;

public record GetTaskMasterByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TaskMasterState>>;

public class GetTaskMasterByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TaskMasterState, GetTaskMasterByIdQuery>, IRequestHandler<GetTaskMasterByIdQuery, Option<TaskMasterState>>
{
    public GetTaskMasterByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TaskMasterState>> Handle(GetTaskMasterByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TaskMaster
			.Include(l=>l.TaskCompanyAssignmentList)
			.Include(l=>l.TaskTagList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
