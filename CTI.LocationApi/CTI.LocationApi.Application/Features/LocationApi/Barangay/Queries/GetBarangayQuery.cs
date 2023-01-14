using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Barangay.Queries;

public record GetBarangayQuery : BaseQuery, IRequest<PagedListResponse<BarangayState>>
{
    public string? ProvinceName { get; set; }
    public string? CityName { get; set; }
}
public class GetBarangayQueryHandler : BaseQueryHandler<ApplicationContext, BarangayState, GetBarangayQuery>, IRequestHandler<GetBarangayQuery, PagedListResponse<BarangayState>>
{
    public GetBarangayQueryHandler(ApplicationContext context) : base(context)
    {
    }

    public override async Task<PagedListResponse<BarangayState>> Handle(GetBarangayQuery request, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<BarangayState>().Include(l => l.City).ThenInclude(l => l.Province)
            .AsNoTracking();
        if (!string.IsNullOrEmpty(request.ProvinceName))
        {
            query = query.Where(l => l.City!.Province!.Name == request.ProvinceName);
        }
        if (!string.IsNullOrEmpty(request.CityName))
        {
            query = query.Where(l => l.City!.Name! == request.CityName);
        }
        return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
                request.SortColumn, request.SortOrder,
                request.PageNumber, request.PageSize,
                cancellationToken);
    }
}
