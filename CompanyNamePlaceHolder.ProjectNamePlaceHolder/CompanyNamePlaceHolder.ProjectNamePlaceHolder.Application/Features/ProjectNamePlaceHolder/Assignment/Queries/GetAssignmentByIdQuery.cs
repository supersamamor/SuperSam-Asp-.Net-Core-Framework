using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Assignment.Queries;

public record GetAssignmentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<AssignmentState>>;

public class GetAssignmentByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, AssignmentState, GetAssignmentByIdQuery>, IRequestHandler<GetAssignmentByIdQuery, Option<AssignmentState>>
{
    public GetAssignmentByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<AssignmentState>> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Assignment.Include(l=>l.TaskList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
