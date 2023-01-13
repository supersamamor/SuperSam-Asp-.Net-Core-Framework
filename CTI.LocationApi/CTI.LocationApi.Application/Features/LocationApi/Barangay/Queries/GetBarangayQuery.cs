using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Barangay.Queries;

public record GetBarangayQuery : BaseQuery, IRequest<PagedListResponse<BarangayState>>;

public class GetBarangayQueryHandler : BaseQueryHandler<ApplicationContext, BarangayState, GetBarangayQuery>, IRequestHandler<GetBarangayQuery, PagedListResponse<BarangayState>>
{
    public GetBarangayQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<BarangayState>> Handle(GetBarangayQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<BarangayState>().Include(l=>l.City)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
