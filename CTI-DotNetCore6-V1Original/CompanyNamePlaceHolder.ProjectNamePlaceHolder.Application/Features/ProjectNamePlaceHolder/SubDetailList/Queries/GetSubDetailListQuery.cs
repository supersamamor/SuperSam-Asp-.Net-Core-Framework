using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Queries;

public record GetSubDetailListQuery : BaseQuery, IRequest<PagedListResponse<SubDetailListState>>;

public class GetSubDetailListQueryHandler : BaseQueryHandler<ApplicationContext, SubDetailListState, GetSubDetailListQuery>, IRequestHandler<GetSubDetailListQuery, PagedListResponse<SubDetailListState>>
{
    public GetSubDetailListQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
