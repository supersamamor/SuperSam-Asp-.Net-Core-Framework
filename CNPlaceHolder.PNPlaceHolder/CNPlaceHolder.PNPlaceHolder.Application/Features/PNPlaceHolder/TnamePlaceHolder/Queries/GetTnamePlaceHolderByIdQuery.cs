using CNPlaceHolder.Common.Core.Queries;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Queries;

public record GetTnamePlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TnamePlaceHolderState>>;

public class GetTnamePlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TnamePlaceHolderState, GetTnamePlaceHolderByIdQuery>, IRequestHandler<GetTnamePlaceHolderByIdQuery, Option<TnamePlaceHolderState>>
{
    public GetTnamePlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
