using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Queries;
public record GetWebContentByPageNameQuery(string PageName) : IRequest<Option<WebContentState>>;
public class GetWebContentByPageNameQueryHandler : IRequestHandler<GetWebContentByPageNameQuery, Option<WebContentState>>
{
    private readonly ApplicationContext _context;

    public GetWebContentByPageNameQueryHandler(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<Option<WebContentState>> Handle(GetWebContentByPageNameQuery request, CancellationToken cancellationToken = default)
    {
        return await _context.WebContent
            .Where(e => e.PageName == request.PageName).IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }
}
