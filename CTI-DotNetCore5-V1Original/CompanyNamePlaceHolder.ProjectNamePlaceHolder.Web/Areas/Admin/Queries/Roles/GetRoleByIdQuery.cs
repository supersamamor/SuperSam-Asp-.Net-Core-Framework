using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles
{
    public record GetRoleByIdQuery(string Id) : IRequest<Option<IdentityRole>>;

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Option<IdentityRole>>
    {
        private readonly IdentityContext _context;

        public GetRoleByIdQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Option<IdentityRole>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken) =>
            await _context.Roles.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
    }
}
