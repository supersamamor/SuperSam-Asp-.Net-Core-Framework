using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Tags.Queries;

public record GetTagsByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TagsState>>;

public class GetTagsByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TagsState, GetTagsByIdQuery>, IRequestHandler<GetTagsByIdQuery, Option<TagsState>>
{
    public GetTagsByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TagsState>> Handle(GetTagsByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Tags
			.Include(l=>l.TaskTagList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
