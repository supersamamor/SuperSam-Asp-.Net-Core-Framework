using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Queries;

public record GetSupplierQuery : BaseQuery, IRequest<PagedListResponse<SupplierState>>;

public class GetSupplierQueryHandler : BaseQueryHandler<ApplicationContext, SupplierState, GetSupplierQuery>, IRequestHandler<GetSupplierQuery, PagedListResponse<SupplierState>>
{
    public GetSupplierQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
