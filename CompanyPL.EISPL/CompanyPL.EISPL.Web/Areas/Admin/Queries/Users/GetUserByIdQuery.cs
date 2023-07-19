using CompanyPL.EISPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CompanyPL.Common.Utility.Helpers.OptionHelper;
using CompanyPL.EISPL.Core.Identity;

namespace CompanyPL.EISPL.Web.Areas.Admin.Queries.Users;

public record GetUserByIdQuery(string Id) : IRequest<Option<ApplicationUser>>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Option<ApplicationUser>>
{
    private readonly IdentityContext _context;

    public GetUserByIdQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Option<ApplicationUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        ToOption(await _context.Users.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken));
}
