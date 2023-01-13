using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Province.Queries;

public record GetProvinceQuery : BaseQuery, IRequest<PagedListResponse<ProvinceState>>;

public class GetProvinceQueryHandler : BaseQueryHandler<ApplicationContext, ProvinceState, GetProvinceQuery>, IRequestHandler<GetProvinceQuery, PagedListResponse<ProvinceState>>
{
    public GetProvinceQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ProvinceState>> Handle(GetProvinceQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ProvinceState>().Include(l=>l.Region)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
