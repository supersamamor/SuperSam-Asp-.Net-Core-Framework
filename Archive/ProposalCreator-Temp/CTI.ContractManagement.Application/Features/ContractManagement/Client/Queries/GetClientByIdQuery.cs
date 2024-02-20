using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Client.Queries;

public record GetClientByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ClientState>>;

public class GetClientByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ClientState, GetClientByIdQuery>, IRequestHandler<GetClientByIdQuery, Option<ClientState>>
{
    public GetClientByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
