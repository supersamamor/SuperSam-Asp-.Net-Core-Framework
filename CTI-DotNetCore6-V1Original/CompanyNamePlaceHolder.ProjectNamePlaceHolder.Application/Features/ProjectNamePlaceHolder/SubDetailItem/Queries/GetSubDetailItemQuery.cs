using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Queries;

public record GetSubDetailItemQuery : BaseQuery, IRequest<PagedListResponse<SubDetailItemState>>;

public class GetSubDetailItemQueryHandler : BaseQueryHandler<ApplicationContext, SubDetailItemState, GetSubDetailItemQuery>, IRequestHandler<GetSubDetailItemQuery, PagedListResponse<SubDetailItemState>>
{
    public GetSubDetailItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
