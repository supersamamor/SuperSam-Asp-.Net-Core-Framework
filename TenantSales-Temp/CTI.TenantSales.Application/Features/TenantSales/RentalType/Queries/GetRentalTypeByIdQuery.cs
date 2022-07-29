using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.RentalType.Queries;

public record GetRentalTypeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<RentalTypeState>>;

public class GetRentalTypeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, RentalTypeState, GetRentalTypeByIdQuery>, IRequestHandler<GetRentalTypeByIdQuery, Option<RentalTypeState>>
{
    public GetRentalTypeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
