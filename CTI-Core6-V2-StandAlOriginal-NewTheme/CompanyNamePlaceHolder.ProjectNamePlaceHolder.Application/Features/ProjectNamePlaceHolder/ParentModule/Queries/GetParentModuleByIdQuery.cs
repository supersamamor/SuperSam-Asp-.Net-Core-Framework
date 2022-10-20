using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Queries;

public record GetParentModuleByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ParentModuleState>>;

public class GetParentModuleByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ParentModuleState, GetParentModuleByIdQuery>, IRequestHandler<GetParentModuleByIdQuery, Option<ParentModuleState>>
{
    public GetParentModuleByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
