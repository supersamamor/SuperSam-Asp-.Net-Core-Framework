using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CTI.Common.Utility.Helpers.OptionHelper;
using CTI.FAS.Core.Identity;

namespace CTI.FAS.Web.Areas.Admin.Queries.Group;

public record GetGroupByIdQuery(string Id) : IRequest<Option<Core.Identity.Group>>;

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, Option<Core.Identity.Group>>
{
    private readonly IdentityContext _context;

    public GetGroupByIdQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Option<Core.Identity.Group>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken) =>
       ToOption(await _context.Group.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken));
}
