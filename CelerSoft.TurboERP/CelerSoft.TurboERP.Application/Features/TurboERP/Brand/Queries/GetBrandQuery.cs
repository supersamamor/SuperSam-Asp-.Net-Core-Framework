using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Brand.Queries;

public record GetBrandQuery : BaseQuery, IRequest<PagedListResponse<BrandState>>;

public class GetBrandQueryHandler : BaseQueryHandler<ApplicationContext, BrandState, GetBrandQuery>, IRequestHandler<GetBrandQuery, PagedListResponse<BrandState>>
{
    public GetBrandQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
