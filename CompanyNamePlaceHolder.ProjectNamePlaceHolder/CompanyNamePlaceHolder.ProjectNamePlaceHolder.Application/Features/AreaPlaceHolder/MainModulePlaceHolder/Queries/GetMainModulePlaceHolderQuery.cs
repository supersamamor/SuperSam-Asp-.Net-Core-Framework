using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

public record GetMainModulePlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<MainModulePlaceHolderState>>;

public class GetMainModulePlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, MainModulePlaceHolderState, GetMainModulePlaceHolderQuery>, IRequestHandler<GetMainModulePlaceHolderQuery, PagedListResponse<MainModulePlaceHolderState>>
{
    public GetMainModulePlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
