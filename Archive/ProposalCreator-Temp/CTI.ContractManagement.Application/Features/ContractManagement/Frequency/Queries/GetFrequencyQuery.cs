using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Queries;

public record GetFrequencyQuery : BaseQuery, IRequest<PagedListResponse<FrequencyState>>;

public class GetFrequencyQueryHandler : BaseQueryHandler<ApplicationContext, FrequencyState, GetFrequencyQuery>, IRequestHandler<GetFrequencyQuery, PagedListResponse<FrequencyState>>
{
    public GetFrequencyQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
