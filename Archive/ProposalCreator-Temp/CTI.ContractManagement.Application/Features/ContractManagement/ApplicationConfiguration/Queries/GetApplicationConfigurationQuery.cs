using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Queries;

public record GetApplicationConfigurationQuery : BaseQuery, IRequest<PagedListResponse<ApplicationConfigurationState>>;

public class GetApplicationConfigurationQueryHandler : BaseQueryHandler<ApplicationContext, ApplicationConfigurationState, GetApplicationConfigurationQuery>, IRequestHandler<GetApplicationConfigurationQuery, PagedListResponse<ApplicationConfigurationState>>
{
    public GetApplicationConfigurationQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
