using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Salutation.Queries;

public record GetSalutationQuery : BaseQuery, IRequest<PagedListResponse<SalutationState>>;

public class GetSalutationQueryHandler : BaseQueryHandler<ApplicationContext, SalutationState, GetSalutationQuery>, IRequestHandler<GetSalutationQuery, PagedListResponse<SalutationState>>
{
    public GetSalutationQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
