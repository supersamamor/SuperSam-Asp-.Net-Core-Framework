using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Queries;

public record GetWebContentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<WebContentState>>;

public class GetWebContentByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, WebContentState, GetWebContentByIdQuery>, IRequestHandler<GetWebContentByIdQuery, Option<WebContentState>>
{
    public GetWebContentByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
