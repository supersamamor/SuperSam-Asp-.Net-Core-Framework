using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Queries;

public record GetHealthDeclarationQuery : BaseQuery, IRequest<PagedListResponse<HealthDeclarationState>>;

public class GetHealthDeclarationQueryHandler : BaseQueryHandler<ApplicationContext, HealthDeclarationState, GetHealthDeclarationQuery>, IRequestHandler<GetHealthDeclarationQuery, PagedListResponse<HealthDeclarationState>>
{
    public GetHealthDeclarationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<HealthDeclarationState>> Handle(GetHealthDeclarationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<HealthDeclarationState>().Include(l=>l.Employee)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
