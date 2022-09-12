using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Queries;

public record GetApplicationConfigurationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ApplicationConfigurationState>>;

public class GetApplicationConfigurationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ApplicationConfigurationState, GetApplicationConfigurationByIdQuery>, IRequestHandler<GetApplicationConfigurationByIdQuery, Option<ApplicationConfigurationState>>
{
    public GetApplicationConfigurationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
