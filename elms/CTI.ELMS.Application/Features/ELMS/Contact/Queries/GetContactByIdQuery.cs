using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Contact.Queries;

public record GetContactByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ContactState>>;

public class GetContactByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ContactState, GetContactByIdQuery>, IRequestHandler<GetContactByIdQuery, Option<ContactState>>
{
    public GetContactByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ContactState>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Contact.Include(l=>l.Lead)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
