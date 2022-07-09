using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CTI.Common.Utility.Helpers.OptionHelper;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;

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
