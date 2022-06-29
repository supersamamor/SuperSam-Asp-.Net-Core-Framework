using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries;

public record GetMainModulePlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<MainModulePlaceHolderState>>;

public class GetMainModulePlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, MainModulePlaceHolderState, GetMainModulePlaceHolderQuery>, IRequestHandler<GetMainModulePlaceHolderQuery, PagedListResponse<MainModulePlaceHolderState>>
{
    public GetMainModulePlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
