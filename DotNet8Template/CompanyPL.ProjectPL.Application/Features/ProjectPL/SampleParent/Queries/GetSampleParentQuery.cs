using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanyPL.ProjectPL.Application.DTOs;
using LanguageExt;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Queries;

public record GetSampleParentQuery : BaseQuery, IRequest<PagedListResponse<SampleParentListDto>>;

public class GetSampleParentQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, SampleParentListDto, GetSampleParentQuery>(context), IRequestHandler<GetSampleParentQuery, PagedListResponse<SampleParentListDto>>
{
	public override async Task<PagedListResponse<SampleParentListDto>> Handle(GetSampleParentQuery request, CancellationToken cancellationToken = default)
	{
		
		return await Context.Set<SampleParentState>()
			.AsNoTracking().Select(e => new SampleParentListDto()
			{
				Id = e.Id,
				LastModifiedDate = e.LastModifiedDate,
				Name = e.Name,
			})
			.ToPagedResponse(request.SearchColumns, request.SearchValue,
				request.SortColumn, request.SortOrder,
				request.PageNumber, request.PageSize,
				cancellationToken);	
	}
}
