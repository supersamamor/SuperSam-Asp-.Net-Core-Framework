using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.OperationType.Queries;

public record GetOperationTypeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<OperationTypeState>>;

public class GetOperationTypeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, OperationTypeState, GetOperationTypeByIdQuery>, IRequestHandler<GetOperationTypeByIdQuery, Option<OperationTypeState>>
{
    public GetOperationTypeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
