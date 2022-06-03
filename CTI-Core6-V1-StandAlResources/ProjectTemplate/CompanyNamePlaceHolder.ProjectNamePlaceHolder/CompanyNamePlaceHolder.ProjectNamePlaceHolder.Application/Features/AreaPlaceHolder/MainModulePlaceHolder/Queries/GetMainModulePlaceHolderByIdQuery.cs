using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

public record GetMainModulePlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<MainModulePlaceHolderState>>;

public class GetMainModulePlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, MainModulePlaceHolderState, GetMainModulePlaceHolderByIdQuery>, IRequestHandler<GetMainModulePlaceHolderByIdQuery, Option<MainModulePlaceHolderState>>
{
    public GetMainModulePlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
