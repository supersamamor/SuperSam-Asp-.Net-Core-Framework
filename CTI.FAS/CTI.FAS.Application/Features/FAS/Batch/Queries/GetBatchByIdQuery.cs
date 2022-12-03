using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Batch.Queries;

public record GetBatchByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BatchState>>;

public class GetBatchByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BatchState, GetBatchByIdQuery>, IRequestHandler<GetBatchByIdQuery, Option<BatchState>>
{
    public GetBatchByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
