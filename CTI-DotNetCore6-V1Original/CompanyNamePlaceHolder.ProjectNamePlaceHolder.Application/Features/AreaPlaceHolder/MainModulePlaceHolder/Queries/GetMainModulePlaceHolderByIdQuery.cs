using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

public record GetMainModulePlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectState>>;

public class GetMainModulePlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectState, GetMainModulePlaceHolderByIdQuery>, IRequestHandler<GetMainModulePlaceHolderByIdQuery, Option<ProjectState>>
{
    public GetMainModulePlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
