using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.TaskTag.Queries;

public record GetTaskTagByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TaskTagState>>;

public class GetTaskTagByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TaskTagState, GetTaskTagByIdQuery>, IRequestHandler<GetTaskTagByIdQuery, Option<TaskTagState>>
{
    public GetTaskTagByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TaskTagState>> Handle(GetTaskTagByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.TaskTag.Include(l=>l.TaskList).Include(l=>l.Tags)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
