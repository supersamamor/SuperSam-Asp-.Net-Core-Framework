using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.CheckReleaseOption.Queries;

public record GetCheckReleaseOptionQuery : BaseQuery, IRequest<PagedListResponse<CheckReleaseOptionState>>;

public class GetCheckReleaseOptionQueryHandler : BaseQueryHandler<ApplicationContext, CheckReleaseOptionState, GetCheckReleaseOptionQuery>, IRequestHandler<GetCheckReleaseOptionQuery, PagedListResponse<CheckReleaseOptionState>>
{
    public GetCheckReleaseOptionQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CheckReleaseOptionState>> Handle(GetCheckReleaseOptionQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CheckReleaseOptionState>().Include(l=>l.Creditor)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
