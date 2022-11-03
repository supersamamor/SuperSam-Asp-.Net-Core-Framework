using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Queries;

public record GetIFCAARLedgerByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<IFCAARLedgerState>>;

public class GetIFCAARLedgerByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, IFCAARLedgerState, GetIFCAARLedgerByIdQuery>, IRequestHandler<GetIFCAARLedgerByIdQuery, Option<IFCAARLedgerState>>
{
    public GetIFCAARLedgerByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<IFCAARLedgerState>> Handle(GetIFCAARLedgerByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.IFCAARLedger.Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
