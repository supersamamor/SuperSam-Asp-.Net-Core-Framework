using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users
{
    public record GetUsersQuery : BaseQuery, IRequest<PagedListResponse<ApplicationUser>>
    {
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedListResponse<ApplicationUser>>
    {
        private readonly IdentityContext _context;

        public GetUsersQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<ApplicationUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken) =>
            await _context.Users.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                request.SortColumn, request.SortOrder,
                                                                request.PageNumber, request.PageSize, cancellationToken);
    }
}
