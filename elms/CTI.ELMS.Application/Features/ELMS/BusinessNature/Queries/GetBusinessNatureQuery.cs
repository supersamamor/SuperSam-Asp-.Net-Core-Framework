using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.BusinessNature.Queries;

public record GetBusinessNatureQuery : BaseQuery, IRequest<PagedListResponse<BusinessNatureState>>;

public class GetBusinessNatureQueryHandler : BaseQueryHandler<ApplicationContext, BusinessNatureState, GetBusinessNatureQuery>, IRequestHandler<GetBusinessNatureQuery, PagedListResponse<BusinessNatureState>>
{
    public GetBusinessNatureQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
