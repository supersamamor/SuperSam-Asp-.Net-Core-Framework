using CNPlaceHolder.Common.Core.Queries;
using CNPlaceHolder.Common.Utility.Models;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using MediatR;
using CNPlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Queries;

public record GetTnamePlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<TnamePlaceHolderState>>;

public class GetTnamePlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, TnamePlaceHolderState, GetTnamePlaceHolderQuery>, IRequestHandler<GetTnamePlaceHolderQuery, PagedListResponse<TnamePlaceHolderState>>
{
    public GetTnamePlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
