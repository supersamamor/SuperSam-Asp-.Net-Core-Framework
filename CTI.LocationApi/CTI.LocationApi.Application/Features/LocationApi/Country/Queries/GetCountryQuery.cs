using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Country.Queries;

public record GetCountryQuery : BaseQuery, IRequest<PagedListResponse<CountryState>>;

public class GetCountryQueryHandler : BaseQueryHandler<ApplicationContext, CountryState, GetCountryQuery>, IRequestHandler<GetCountryQuery, PagedListResponse<CountryState>>
{
    public GetCountryQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
