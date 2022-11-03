using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Queries;

public record GetUserProjectAssignmentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UserProjectAssignmentState>>;

public class GetUserProjectAssignmentByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UserProjectAssignmentState, GetUserProjectAssignmentByIdQuery>, IRequestHandler<GetUserProjectAssignmentByIdQuery, Option<UserProjectAssignmentState>>
{
    public GetUserProjectAssignmentByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UserProjectAssignmentState>> Handle(GetUserProjectAssignmentByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UserProjectAssignment.Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
