using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadSource.Queries;

public record GetLeadSourceQuery : BaseQuery, IRequest<PagedListResponse<LeadSourceState>>;

public class GetLeadSourceQueryHandler : BaseQueryHandler<ApplicationContext, LeadSourceState, GetLeadSourceQuery>, IRequestHandler<GetLeadSourceQuery, PagedListResponse<LeadSourceState>>
{
    public GetLeadSourceQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
