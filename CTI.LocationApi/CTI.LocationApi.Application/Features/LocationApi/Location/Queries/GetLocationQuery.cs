using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Location.Queries;

public record GetLocationQuery : BaseQuery, IRequest<PagedListResponse<LocationState>>;

public class GetLocationQueryHandler : BaseQueryHandler<ApplicationContext, LocationState, GetLocationQuery>, IRequestHandler<GetLocationQuery, PagedListResponse<LocationState>>
{
    public GetLocationQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
