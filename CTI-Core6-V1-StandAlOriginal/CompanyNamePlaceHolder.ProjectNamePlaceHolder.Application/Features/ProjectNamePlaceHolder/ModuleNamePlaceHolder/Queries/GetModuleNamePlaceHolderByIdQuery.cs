using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Queries;

public record GetModuleNamePlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ModuleNamePlaceHolderState>>;

public class GetModuleNamePlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ModuleNamePlaceHolderState, GetModuleNamePlaceHolderByIdQuery>, IRequestHandler<GetModuleNamePlaceHolderByIdQuery, Option<ModuleNamePlaceHolderState>>
{
    public GetModuleNamePlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
