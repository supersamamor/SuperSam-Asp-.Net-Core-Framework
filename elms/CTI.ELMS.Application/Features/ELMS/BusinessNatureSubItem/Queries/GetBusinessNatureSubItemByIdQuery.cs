using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Queries;

public record GetBusinessNatureSubItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BusinessNatureSubItemState>>;

public class GetBusinessNatureSubItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BusinessNatureSubItemState, GetBusinessNatureSubItemByIdQuery>, IRequestHandler<GetBusinessNatureSubItemByIdQuery, Option<BusinessNatureSubItemState>>
{
    public GetBusinessNatureSubItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<BusinessNatureSubItemState>> Handle(GetBusinessNatureSubItemByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.BusinessNatureSubItem.Include(l=>l.BusinessNature)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
