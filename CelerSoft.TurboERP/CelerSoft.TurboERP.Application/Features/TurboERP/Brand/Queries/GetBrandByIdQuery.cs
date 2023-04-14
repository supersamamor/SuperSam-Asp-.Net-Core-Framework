using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Brand.Queries;

public record GetBrandByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BrandState>>;

public class GetBrandByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BrandState, GetBrandByIdQuery>, IRequestHandler<GetBrandByIdQuery, Option<BrandState>>
{
    public GetBrandByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
