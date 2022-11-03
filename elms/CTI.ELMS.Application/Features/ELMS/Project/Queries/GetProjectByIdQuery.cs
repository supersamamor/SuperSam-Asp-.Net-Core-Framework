using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Project.Queries;

public record GetProjectByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectState>>;

public class GetProjectByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectState, GetProjectByIdQuery>, IRequestHandler<GetProjectByIdQuery, Option<ProjectState>>
{
    public GetProjectByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProjectState>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Project.Include(l=>l.EntityGroup)
			.Include(l=>l.OfferingHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
