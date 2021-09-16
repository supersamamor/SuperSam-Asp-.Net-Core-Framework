using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users
{
    public record GetUserByIdQuery(string Id) : IRequest<Option<ApplicationUser>>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Option<ApplicationUser>>
    {
        private readonly IdentityContext _context;

        public GetUserByIdQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Option<ApplicationUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
            await _context.Users.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
    }
}
