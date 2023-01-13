using CTI.Common.Core.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Location.Queries;

public record GetLocationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LocationState>>;

public class GetLocationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LocationState, GetLocationByIdQuery>, IRequestHandler<GetLocationByIdQuery, Option<LocationState>>
{
    public GetLocationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
