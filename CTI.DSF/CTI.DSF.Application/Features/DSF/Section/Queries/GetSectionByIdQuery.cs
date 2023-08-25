using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Section.Queries;

public record GetSectionByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SectionState>>;

public class GetSectionByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SectionState, GetSectionByIdQuery>, IRequestHandler<GetSectionByIdQuery, Option<SectionState>>
{
    public GetSectionByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SectionState>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Section.Include(l=>l.Department)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
