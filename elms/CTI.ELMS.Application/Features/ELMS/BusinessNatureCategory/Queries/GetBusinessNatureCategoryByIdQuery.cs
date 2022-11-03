using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Queries;

public record GetBusinessNatureCategoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BusinessNatureCategoryState>>;

public class GetBusinessNatureCategoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BusinessNatureCategoryState, GetBusinessNatureCategoryByIdQuery>, IRequestHandler<GetBusinessNatureCategoryByIdQuery, Option<BusinessNatureCategoryState>>
{
    public GetBusinessNatureCategoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<BusinessNatureCategoryState>> Handle(GetBusinessNatureCategoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.BusinessNatureCategory.Include(l=>l.BusinessNatureSubItem)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
