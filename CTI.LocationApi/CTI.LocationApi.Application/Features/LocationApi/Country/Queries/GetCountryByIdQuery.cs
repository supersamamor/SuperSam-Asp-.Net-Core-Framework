using CTI.Common.Core.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Country.Queries;

public record GetCountryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CountryState>>;

public class GetCountryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CountryState, GetCountryByIdQuery>, IRequestHandler<GetCountryByIdQuery, Option<CountryState>>
{
    public GetCountryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
