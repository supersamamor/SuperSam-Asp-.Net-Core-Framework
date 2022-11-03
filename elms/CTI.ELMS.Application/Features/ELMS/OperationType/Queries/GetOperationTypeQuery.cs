using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.OperationType.Queries;

public record GetOperationTypeQuery : BaseQuery, IRequest<PagedListResponse<OperationTypeState>>;

public class GetOperationTypeQueryHandler : BaseQueryHandler<ApplicationContext, OperationTypeState, GetOperationTypeQuery>, IRequestHandler<GetOperationTypeQuery, PagedListResponse<OperationTypeState>>
{
    public GetOperationTypeQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
