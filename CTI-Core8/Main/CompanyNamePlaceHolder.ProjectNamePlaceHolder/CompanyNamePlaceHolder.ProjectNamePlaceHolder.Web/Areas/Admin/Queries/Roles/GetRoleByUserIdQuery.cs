using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;

public record GetRoleByUserIdQuery(string? UserId) : IRequest<string?>;

public class GetRoleByUserIdQueryHandler(IdentityContext context) : IRequestHandler<GetRoleByUserIdQuery, string?>
{
    public async Task<string?> Handle(GetRoleByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await (from r in context.Roles
                           join ur in context.UserRoles on r.Id equals ur.RoleId
                           where ur.UserId == request.UserId && r.Name != Core.Constants.Roles.User
                      select r.Name).AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

}
