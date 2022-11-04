using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Lead.Queries;

public record GetLeadByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LeadState>>;

public class GetLeadByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LeadState, GetLeadByIdQuery>, IRequestHandler<GetLeadByIdQuery, Option<LeadState>>
{
    public GetLeadByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<LeadState>> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Lead.Include(l=>l.BusinessNatureCategory).Include(l=>l.BusinessNatureSubItem).Include(l=>l.LeadSource).Include(l=>l.BusinessNature).Include(l=>l.OperationType).Include(l=>l.LeadTouchPoint)
			.Include(l=>l.ContactList)
			.Include(l=>l.ContactPersonList)
			.Include(l=>l.OfferingHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
