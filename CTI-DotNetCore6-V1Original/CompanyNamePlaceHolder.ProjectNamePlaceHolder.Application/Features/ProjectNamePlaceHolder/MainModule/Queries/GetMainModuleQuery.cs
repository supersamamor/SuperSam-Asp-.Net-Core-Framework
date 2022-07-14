using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Queries;

public record GetMainModuleQuery : BaseQuery, IRequest<PagedListResponse<MainModuleState>>;

public class GetMainModuleQueryHandler : BaseQueryHandler<ApplicationContext, MainModuleState, GetMainModuleQuery>, IRequestHandler<GetMainModuleQuery, PagedListResponse<MainModuleState>>
{
    public GetMainModuleQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
