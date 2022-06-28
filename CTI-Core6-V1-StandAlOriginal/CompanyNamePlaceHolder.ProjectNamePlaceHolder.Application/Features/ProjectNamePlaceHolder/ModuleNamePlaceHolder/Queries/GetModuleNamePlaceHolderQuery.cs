using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Queries;

public record GetModuleNamePlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<ModuleNamePlaceHolderState>>;

public class GetModuleNamePlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, ModuleNamePlaceHolderState, GetModuleNamePlaceHolderQuery>, IRequestHandler<GetModuleNamePlaceHolderQuery, PagedListResponse<ModuleNamePlaceHolderState>>
{
    public GetModuleNamePlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
