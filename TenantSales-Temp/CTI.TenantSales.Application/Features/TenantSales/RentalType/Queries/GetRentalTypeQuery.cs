using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.RentalType.Queries;

public record GetRentalTypeQuery : BaseQuery, IRequest<PagedListResponse<RentalTypeState>>;

public class GetRentalTypeQueryHandler : BaseQueryHandler<ApplicationContext, RentalTypeState, GetRentalTypeQuery>, IRequestHandler<GetRentalTypeQuery, PagedListResponse<RentalTypeState>>
{
    public GetRentalTypeQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
