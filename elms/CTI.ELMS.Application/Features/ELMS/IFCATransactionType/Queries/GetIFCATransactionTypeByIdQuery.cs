using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Queries;

public record GetIFCATransactionTypeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<IFCATransactionTypeState>>;

public class GetIFCATransactionTypeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, IFCATransactionTypeState, GetIFCATransactionTypeByIdQuery>, IRequestHandler<GetIFCATransactionTypeByIdQuery, Option<IFCATransactionTypeState>>
{
    public GetIFCATransactionTypeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<IFCATransactionTypeState>> Handle(GetIFCATransactionTypeByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.IFCATransactionType.Include(l=>l.EntityGroup)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
