using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Batch.Queries;

public record GetBatchQuery : BaseQuery, IRequest<PagedListResponse<BatchState>>;

public class GetBatchQueryHandler : BaseQueryHandler<ApplicationContext, BatchState, GetBatchQuery>, IRequestHandler<GetBatchQuery, PagedListResponse<BatchState>>
{
    public GetBatchQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
