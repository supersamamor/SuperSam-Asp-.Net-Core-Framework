using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Queries;

public record GetUploadStagingQuery : BaseQuery, IRequest<PagedListResponse<UploadStagingState>>;

public class GetUploadStagingQueryHandler : BaseQueryHandler<ApplicationContext, UploadStagingState, GetUploadStagingQuery>, IRequestHandler<GetUploadStagingQuery, PagedListResponse<UploadStagingState>>
{
    public GetUploadStagingQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UploadStagingState>> Handle(GetUploadStagingQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UploadStagingState>().Include(l=>l.UploadProcessor)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
