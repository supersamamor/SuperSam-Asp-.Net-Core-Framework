using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Queries;

public record GetSubDetailListPlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<SubDetailListPlaceHolderState>>;

public class GetSubDetailListPlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, SubDetailListPlaceHolderState, GetSubDetailListPlaceHolderQuery>, IRequestHandler<GetSubDetailListPlaceHolderQuery, PagedListResponse<SubDetailListPlaceHolderState>>
{
    public GetSubDetailListPlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
