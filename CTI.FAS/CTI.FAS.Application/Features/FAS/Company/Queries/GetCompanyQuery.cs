using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Company.Queries;

public record GetCompanyQuery : BaseQuery, IRequest<PagedListResponse<CompanyState>>;

public class GetCompanyQueryHandler : BaseQueryHandler<ApplicationContext, CompanyState, GetCompanyQuery>, IRequestHandler<GetCompanyQuery, PagedListResponse<CompanyState>>
{
    public GetCompanyQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CompanyState>> Handle(GetCompanyQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CompanyState>().Include(l=>l.DatabaseConnectionSetup)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
