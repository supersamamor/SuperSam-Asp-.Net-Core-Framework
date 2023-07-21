using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Queries;

public record GetPLHealthDeclarationQuery : BaseQuery, IRequest<PagedListResponse<PLHealthDeclarationState>>;

public class GetPLHealthDeclarationQueryHandler : BaseQueryHandler<ApplicationContext, PLHealthDeclarationState, GetPLHealthDeclarationQuery>, IRequestHandler<GetPLHealthDeclarationQuery, PagedListResponse<PLHealthDeclarationState>>
{
    public GetPLHealthDeclarationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<PLHealthDeclarationState>> Handle(GetPLHealthDeclarationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<PLHealthDeclarationState>().Include(l=>l.PLEmployee)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
