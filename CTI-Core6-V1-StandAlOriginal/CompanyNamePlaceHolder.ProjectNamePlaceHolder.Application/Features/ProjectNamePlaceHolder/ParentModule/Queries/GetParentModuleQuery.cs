using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Queries;

public record GetParentModuleQuery : BaseQuery, IRequest<PagedListResponse<ParentModuleState>>;

public class GetParentModuleQueryHandler : BaseQueryHandler<ApplicationContext, ParentModuleState, GetParentModuleQuery>, IRequestHandler<GetParentModuleQuery, PagedListResponse<ParentModuleState>>
{
    public GetParentModuleQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
