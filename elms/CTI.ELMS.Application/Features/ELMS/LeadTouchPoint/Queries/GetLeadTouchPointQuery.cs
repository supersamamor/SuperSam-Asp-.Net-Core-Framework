using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Queries;

public record GetLeadTouchPointQuery : BaseQuery, IRequest<PagedListResponse<LeadTouchPointState>>;

public class GetLeadTouchPointQueryHandler : BaseQueryHandler<ApplicationContext, LeadTouchPointState, GetLeadTouchPointQuery>, IRequestHandler<GetLeadTouchPointQuery, PagedListResponse<LeadTouchPointState>>
{
    public GetLeadTouchPointQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
