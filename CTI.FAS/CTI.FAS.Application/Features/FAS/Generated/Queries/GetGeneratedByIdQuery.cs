using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Generated.Queries;

public record GetGeneratedByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<GeneratedState>>;

public class GetGeneratedByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, GeneratedState, GetGeneratedByIdQuery>, IRequestHandler<GetGeneratedByIdQuery, Option<GeneratedState>>
{
    public GetGeneratedByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<GeneratedState>> Handle(GetGeneratedByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Generated.Include(l=>l.Batch).Include(l=>l.Company).Include(l=>l.Creditor)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
