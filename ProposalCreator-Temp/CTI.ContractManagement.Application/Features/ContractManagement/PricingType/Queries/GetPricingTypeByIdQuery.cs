using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.PricingType.Queries;

public record GetPricingTypeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PricingTypeState>>;

public class GetPricingTypeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PricingTypeState, GetPricingTypeByIdQuery>, IRequestHandler<GetPricingTypeByIdQuery, Option<PricingTypeState>>
{
    public GetPricingTypeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
