using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Theme.Queries;

public record GetThemeQuery : BaseQuery, IRequest<PagedListResponse<ThemeState>>;

public class GetThemeQueryHandler : BaseQueryHandler<ApplicationContext, ThemeState, GetThemeQuery>, IRequestHandler<GetThemeQuery, PagedListResponse<ThemeState>>
{
    public GetThemeQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
