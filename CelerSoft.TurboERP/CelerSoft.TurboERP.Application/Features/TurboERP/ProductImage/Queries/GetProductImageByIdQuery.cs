using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Queries;

public record GetProductImageByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProductImageState>>;

public class GetProductImageByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProductImageState, GetProductImageByIdQuery>, IRequestHandler<GetProductImageByIdQuery, Option<ProductImageState>>
{
    public GetProductImageByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProductImageState>> Handle(GetProductImageByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ProductImage.Include(l=>l.Product)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
