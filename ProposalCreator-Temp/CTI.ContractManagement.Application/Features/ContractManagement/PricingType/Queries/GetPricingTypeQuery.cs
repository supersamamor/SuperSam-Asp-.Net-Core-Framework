using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.PricingType.Queries;

public record GetPricingTypeQuery : BaseQuery, IRequest<PagedListResponse<PricingTypeState>>;

public class GetPricingTypeQueryHandler : BaseQueryHandler<ApplicationContext, PricingTypeState, GetPricingTypeQuery>, IRequestHandler<GetPricingTypeQuery, PagedListResponse<PricingTypeState>>
{
    public GetPricingTypeQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
