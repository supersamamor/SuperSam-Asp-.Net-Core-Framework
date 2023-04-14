using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Queries;

public record GetProductImageQuery : BaseQuery, IRequest<PagedListResponse<ProductImageState>>;

public class GetProductImageQueryHandler : BaseQueryHandler<ApplicationContext, ProductImageState, GetProductImageQuery>, IRequestHandler<GetProductImageQuery, PagedListResponse<ProductImageState>>
{
    public GetProductImageQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProductImageState>> Handle(GetProductImageQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProductImageState>().Include(l=>l.Product)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
