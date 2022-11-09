using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Queries;

public record GetBusinessNatureSubItemQuery : BaseQuery, IRequest<PagedListResponse<BusinessNatureSubItemState>>
{
	public string? BusinessNatureId { get; set; }
}

public class GetBusinessNatureSubItemQueryHandler : BaseQueryHandler<ApplicationContext, BusinessNatureSubItemState, GetBusinessNatureSubItemQuery>, IRequestHandler<GetBusinessNatureSubItemQuery, PagedListResponse<BusinessNatureSubItemState>>
{
    public GetBusinessNatureSubItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<BusinessNatureSubItemState>> Handle(GetBusinessNatureSubItemQuery request, CancellationToken cancellationToken = default)
	{
		var query = Context.BusinessNatureSubItem.Include(l => l.BusinessNature).AsNoTracking();
		if (!string.IsNullOrEmpty(request.BusinessNatureId))
		{
			query = query.Where(l => l.BusinessNatureID == request.BusinessNatureId);
		}
		return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);
	}
		
}
