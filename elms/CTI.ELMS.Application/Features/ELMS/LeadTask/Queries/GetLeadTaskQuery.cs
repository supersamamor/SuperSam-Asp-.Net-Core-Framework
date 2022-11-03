using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTask.Queries;

public record GetLeadTaskQuery : BaseQuery, IRequest<PagedListResponse<LeadTaskState>>;

public class GetLeadTaskQueryHandler : BaseQueryHandler<ApplicationContext, LeadTaskState, GetLeadTaskQuery>, IRequestHandler<GetLeadTaskQuery, PagedListResponse<LeadTaskState>>
{
    public GetLeadTaskQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
