using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Category.Queries;

public record GetCategoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CategoryState>>;

public class GetCategoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CategoryState, GetCategoryByIdQuery>, IRequestHandler<GetCategoryByIdQuery, Option<CategoryState>>
{
    public GetCategoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CategoryState>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Category.Include(l=>l.Classification)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
