using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrement.Queries;

public record GetAnnualIncrementQuery : BaseQuery, IRequest<PagedListResponse<AnnualIncrementState>>;

public class GetAnnualIncrementQueryHandler : BaseQueryHandler<ApplicationContext, AnnualIncrementState, GetAnnualIncrementQuery>, IRequestHandler<GetAnnualIncrementQuery, PagedListResponse<AnnualIncrementState>>
{
    public GetAnnualIncrementQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<AnnualIncrementState>> Handle(GetAnnualIncrementQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<AnnualIncrementState>().Include(l=>l.UnitOffered)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
