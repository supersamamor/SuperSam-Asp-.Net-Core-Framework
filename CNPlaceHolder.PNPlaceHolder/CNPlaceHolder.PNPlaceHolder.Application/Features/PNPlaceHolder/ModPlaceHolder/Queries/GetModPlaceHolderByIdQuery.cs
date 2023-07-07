using CNPlaceHolder.Common.Core.Queries;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Queries;

public record GetModPlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ModPlaceHolderState>>;

public class GetModPlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ModPlaceHolderState, GetModPlaceHolderByIdQuery>, IRequestHandler<GetModPlaceHolderByIdQuery, Option<ModPlaceHolderState>>
{
    public GetModPlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
