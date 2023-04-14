using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Queries;

public record GetWebContentQuery : BaseQuery, IRequest<PagedListResponse<WebContentState>>;

public class GetWebContentQueryHandler : BaseQueryHandler<ApplicationContext, WebContentState, GetWebContentQuery>, IRequestHandler<GetWebContentQuery, PagedListResponse<WebContentState>>
{
    public GetWebContentQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
