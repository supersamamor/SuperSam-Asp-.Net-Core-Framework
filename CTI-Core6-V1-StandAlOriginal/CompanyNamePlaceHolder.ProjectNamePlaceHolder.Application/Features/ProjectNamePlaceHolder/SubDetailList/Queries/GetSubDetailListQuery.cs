using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Queries;

public record GetSubDetailListQuery : BaseQuery, IRequest<PagedListResponse<SubDetailListState>>;

public class GetSubDetailListQueryHandler : BaseQueryHandler<ApplicationContext, SubDetailListState, GetSubDetailListQuery>, IRequestHandler<GetSubDetailListQuery, PagedListResponse<SubDetailListState>>
{
    public GetSubDetailListQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<SubDetailListState>> Handle(GetSubDetailListQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<SubDetailListState>().Include(l=>l.TestForeignKeyOne)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
