using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Queries;

public record GetSupplierContactPersonQuery : BaseQuery, IRequest<PagedListResponse<SupplierContactPersonState>>;

public class GetSupplierContactPersonQueryHandler : BaseQueryHandler<ApplicationContext, SupplierContactPersonState, GetSupplierContactPersonQuery>, IRequestHandler<GetSupplierContactPersonQuery, PagedListResponse<SupplierContactPersonState>>
{
    public GetSupplierContactPersonQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<SupplierContactPersonState>> Handle(GetSupplierContactPersonQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<SupplierContactPersonState>().Include(l=>l.Supplier)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
