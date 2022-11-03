using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Salutation.Queries;

public record GetSalutationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SalutationState>>;

public class GetSalutationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SalutationState, GetSalutationByIdQuery>, IRequestHandler<GetSalutationByIdQuery, Option<SalutationState>>
{
    public GetSalutationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SalutationState>> Handle(GetSalutationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Salutation
			.Include(l=>l.ContactPersonList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
