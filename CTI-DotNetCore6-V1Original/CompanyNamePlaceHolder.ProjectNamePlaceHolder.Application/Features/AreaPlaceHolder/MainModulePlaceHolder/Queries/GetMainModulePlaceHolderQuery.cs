using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

public record GetMainModulePlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<ProjectState>>;

public class GetMainModulePlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, ProjectState, GetMainModulePlaceHolderQuery>, IRequestHandler<GetMainModulePlaceHolderQuery, PagedListResponse<ProjectState>>
{
    public GetMainModulePlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
