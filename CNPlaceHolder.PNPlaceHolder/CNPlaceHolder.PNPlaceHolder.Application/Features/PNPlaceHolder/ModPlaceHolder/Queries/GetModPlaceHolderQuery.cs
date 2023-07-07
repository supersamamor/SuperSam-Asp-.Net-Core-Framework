using CNPlaceHolder.Common.Core.Queries;
using CNPlaceHolder.Common.Utility.Models;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using MediatR;
using CNPlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Queries;

public record GetModPlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<ModPlaceHolderState>>;

public class GetModPlaceHolderQueryHandler : BaseQueryHandler<ApplicationContext, ModPlaceHolderState, GetModPlaceHolderQuery>, IRequestHandler<GetModPlaceHolderQuery, PagedListResponse<ModPlaceHolderState>>
{
    public GetModPlaceHolderQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
