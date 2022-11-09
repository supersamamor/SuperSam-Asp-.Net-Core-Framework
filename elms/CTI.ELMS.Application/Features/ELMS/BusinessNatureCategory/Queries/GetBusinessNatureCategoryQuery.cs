using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Queries;

public record GetBusinessNatureCategoryQuery : BaseQuery, IRequest<PagedListResponse<BusinessNatureCategoryState>>
{
    public string? BusinessNatureSubItemId { get; set; }
}
public class GetBusinessNatureCategoryQueryHandler : BaseQueryHandler<ApplicationContext, BusinessNatureCategoryState, GetBusinessNatureCategoryQuery>, IRequestHandler<GetBusinessNatureCategoryQuery, PagedListResponse<BusinessNatureCategoryState>>
{
    public GetBusinessNatureCategoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<BusinessNatureCategoryState>> Handle(GetBusinessNatureCategoryQuery request, CancellationToken cancellationToken = default)
    {
        var query = Context.BusinessNatureCategory.Include(l => l.BusinessNatureSubItem).AsNoTracking();
        if (!string.IsNullOrEmpty(request.BusinessNatureSubItemId))
        {
            query = query.Where(l => l.BusinessNatureSubItemID == request.BusinessNatureSubItemId);
        }
        return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
            request.SortColumn, request.SortOrder,
            request.PageNumber, request.PageSize,
            cancellationToken);
    }

}
