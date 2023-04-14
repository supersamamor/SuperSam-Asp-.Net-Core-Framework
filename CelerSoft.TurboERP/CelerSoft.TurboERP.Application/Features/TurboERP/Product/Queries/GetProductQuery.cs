using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Product.Queries;

public record GetProductQuery : BaseQuery, IRequest<PagedListResponse<ProductState>>;

public class GetProductQueryHandler : BaseQueryHandler<ApplicationContext, ProductState, GetProductQuery>, IRequestHandler<GetProductQuery, PagedListResponse<ProductState>>
{
    public GetProductQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProductState>> Handle(GetProductQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProductState>().Include(l=>l.Brand).Include(l=>l.Item)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
