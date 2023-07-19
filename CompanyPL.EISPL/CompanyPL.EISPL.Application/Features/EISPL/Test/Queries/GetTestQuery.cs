using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.Test.Queries;

public record GetTestQuery : BaseQuery, IRequest<PagedListResponse<TestState>>;

public class GetTestQueryHandler : BaseQueryHandler<ApplicationContext, TestState, GetTestQuery>, IRequestHandler<GetTestQuery, PagedListResponse<TestState>>
{
    public GetTestQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TestState>> Handle(GetTestQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TestState>().Include(l=>l.PLEmployee)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
