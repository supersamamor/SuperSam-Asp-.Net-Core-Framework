using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;
using LanguageExt;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

public record GetMainModulePlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<MainModulePlaceHolderListDto>>;

public class GetMainModulePlaceHolderQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, MainModulePlaceHolderListDto, GetMainModulePlaceHolderQuery>(context), IRequestHandler<GetMainModulePlaceHolderQuery, PagedListResponse<MainModulePlaceHolderListDto>>
{
	public override async Task<PagedListResponse<MainModulePlaceHolderListDto>> Handle(GetMainModulePlaceHolderQuery request, CancellationToken cancellationToken = default)
	{
		Template:[ListingQueryIncludeParent]
	}
}
