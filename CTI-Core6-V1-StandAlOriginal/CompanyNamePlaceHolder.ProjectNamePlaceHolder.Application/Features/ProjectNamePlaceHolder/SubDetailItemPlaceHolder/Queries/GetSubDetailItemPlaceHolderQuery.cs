using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Queries;

public record GetSubDetailItemPlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<SubDetailItemPlaceHolderState>>;

public class GetSubDetailItemPlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, SubDetailItemPlaceHolderState, GetSubDetailItemPlaceHolderQuery>, IRequestHandler<GetSubDetailItemPlaceHolderQuery, PagedListResponse<SubDetailItemPlaceHolderState>>
{
    public GetSubDetailItemPlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
