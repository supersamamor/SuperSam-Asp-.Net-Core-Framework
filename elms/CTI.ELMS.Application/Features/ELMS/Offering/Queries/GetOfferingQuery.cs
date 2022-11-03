using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Queries;

public record GetOfferingQuery : BaseQuery, IRequest<PagedListResponse<OfferingState>>;

public class GetOfferingQueryHandler : BaseQueryHandler<ApplicationContext, OfferingState, GetOfferingQuery>, IRequestHandler<GetOfferingQuery, PagedListResponse<OfferingState>>
{
    public GetOfferingQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<OfferingState>> Handle(GetOfferingQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<OfferingState>().Include(l=>l.Project).Include(l=>l.Lead)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
