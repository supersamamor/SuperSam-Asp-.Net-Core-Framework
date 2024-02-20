using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Client.Queries;

public record GetClientQuery : BaseQuery, IRequest<PagedListResponse<ClientState>>;

public class GetClientQueryHandler : BaseQueryHandler<ApplicationContext, ClientState, GetClientQuery>, IRequestHandler<GetClientQuery, PagedListResponse<ClientState>>
{
    public GetClientQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
