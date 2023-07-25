using CTI.DPI.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CTI.Common.Utility.Helpers.OptionHelper;
using CTI.DPI.Core.Identity;

namespace CTI.DPI.Web.Areas.Admin.Queries.Entities;

public record GetEntityByIdQuery(string Id) : IRequest<Option<Entity>>;

public class GetEntityByIdQueryHandler : IRequestHandler<GetEntityByIdQuery, Option<Entity>>
{
    private readonly IdentityContext _context;

    public GetEntityByIdQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Option<Entity>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken) =>
       ToOption(await _context.Entities.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken));
}
