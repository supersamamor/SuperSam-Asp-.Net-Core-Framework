using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Queries;

public record GetTaskCompanyAssignmentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TaskCompanyAssignmentState>>;

public class GetTaskCompanyAssignmentByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TaskCompanyAssignmentState, GetTaskCompanyAssignmentByIdQuery>, IRequestHandler<GetTaskCompanyAssignmentByIdQuery, Option<TaskCompanyAssignmentState>>
{
    public GetTaskCompanyAssignmentByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TaskCompanyAssignmentState>> Handle(GetTaskCompanyAssignmentByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TaskCompanyAssignment.Include(l=>l.Team).Include(l=>l.Company).Include(l=>l.Department).Include(l=>l.Section).Include(l=>l.TaskMaster)
			.Include(l=>l.TaskApproverList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
