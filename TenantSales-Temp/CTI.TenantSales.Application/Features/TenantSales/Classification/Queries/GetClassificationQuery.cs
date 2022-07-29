using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Classification.Queries;

public record GetClassificationQuery : BaseQuery, IRequest<PagedListResponse<ClassificationState>>;

public class GetClassificationQueryHandler : BaseQueryHandler<ApplicationContext, ClassificationState, GetClassificationQuery>, IRequestHandler<GetClassificationQuery, PagedListResponse<ClassificationState>>
{
    public GetClassificationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ClassificationState>> Handle(GetClassificationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ClassificationState>().Include(l=>l.Theme)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
