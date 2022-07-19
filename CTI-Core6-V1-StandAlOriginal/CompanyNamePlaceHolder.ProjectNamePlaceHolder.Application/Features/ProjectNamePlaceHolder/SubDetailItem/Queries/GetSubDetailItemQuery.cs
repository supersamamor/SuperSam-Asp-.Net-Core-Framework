using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Queries;

public record GetSubDetailItemQuery : BaseQuery, IRequest<PagedListResponse<SubDetailItemState>>;

public class GetSubDetailItemQueryHandler : BaseQueryHandler<ApplicationContext, SubDetailItemState, GetSubDetailItemQuery>, IRequestHandler<GetSubDetailItemQuery, PagedListResponse<SubDetailItemState>>
{
    public GetSubDetailItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<SubDetailItemState>> Handle(GetSubDetailItemQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<SubDetailItemState>().Include(l=>l.TestForeignKeyTwo)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
