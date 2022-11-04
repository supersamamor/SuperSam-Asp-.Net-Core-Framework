using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ContactPerson.Queries;

public record GetContactPersonByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ContactPersonState>>;

public class GetContactPersonByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ContactPersonState, GetContactPersonByIdQuery>, IRequestHandler<GetContactPersonByIdQuery, Option<ContactPersonState>>
{
    public GetContactPersonByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ContactPersonState>> Handle(GetContactPersonByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ContactPerson.Include(l=>l.Salutation).Include(l=>l.Lead)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
