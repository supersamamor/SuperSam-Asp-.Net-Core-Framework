using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Theme.Queries;

public record GetThemeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ThemeState>>;

public class GetThemeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ThemeState, GetThemeByIdQuery>, IRequestHandler<GetThemeByIdQuery, Option<ThemeState>>
{
    public GetThemeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ThemeState>> Handle(GetThemeByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Theme
			.Include(l=>l.ClassificationList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
