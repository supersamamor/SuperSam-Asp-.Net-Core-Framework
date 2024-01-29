using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CompanyNamePlaceHolder.Common.Utility.Helpers.OptionHelper;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;

public record GetRoleByIdQuery(string Id) : IRequest<Option<ApplicationRole>>;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Option<ApplicationRole>>
{
    private readonly IdentityContext _context;

    public GetRoleByIdQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Option<ApplicationRole>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken) =>
       ToOption(await _context.Roles.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken));
}
