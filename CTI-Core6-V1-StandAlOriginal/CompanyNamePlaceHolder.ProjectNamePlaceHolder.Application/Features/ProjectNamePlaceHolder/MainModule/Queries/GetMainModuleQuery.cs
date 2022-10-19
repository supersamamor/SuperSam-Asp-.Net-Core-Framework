using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Queries;

public record GetMainModuleQuery : BaseQuery, IRequest<PagedListResponse<MainModuleState>>;

public class GetMainModuleQueryHandler : BaseQueryHandler<ApplicationContext, MainModuleState, GetMainModuleQuery>, IRequestHandler<GetMainModuleQuery, PagedListResponse<MainModuleState>>
{
    public GetMainModuleQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<MainModuleState>> Handle(GetMainModuleQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<MainModuleState>().Include(l=>l.ParentModule)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
