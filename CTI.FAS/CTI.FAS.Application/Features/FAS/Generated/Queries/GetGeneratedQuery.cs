using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Generated.Queries;

public record GetGeneratedQuery : BaseQuery, IRequest<PagedListResponse<GeneratedState>>;

public class GetGeneratedQueryHandler : BaseQueryHandler<ApplicationContext, GeneratedState, GetGeneratedQuery>, IRequestHandler<GetGeneratedQuery, PagedListResponse<GeneratedState>>
{
    public GetGeneratedQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<GeneratedState>> Handle(GetGeneratedQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<GeneratedState>().Include(l=>l.Batch).Include(l=>l.Company).Include(l=>l.Creditor)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
