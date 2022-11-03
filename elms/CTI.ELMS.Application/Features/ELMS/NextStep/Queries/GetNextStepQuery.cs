using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.NextStep.Queries;

public record GetNextStepQuery : BaseQuery, IRequest<PagedListResponse<NextStepState>>;

public class GetNextStepQueryHandler : BaseQueryHandler<ApplicationContext, NextStepState, GetNextStepQuery>, IRequestHandler<GetNextStepQuery, PagedListResponse<NextStepState>>
{
    public GetNextStepQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
