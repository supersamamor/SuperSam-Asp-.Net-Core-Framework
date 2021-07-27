using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles
{
    public record GetRolesQuery : BaseQuery, IRequest<PagedListResponse<IdentityRole>>
    {
    }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedListResponse<IdentityRole>>
    {
        private readonly IdentityContext _context;

        public GetRolesQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<IdentityRole>> Handle(GetRolesQuery request, CancellationToken cancellationToken) =>
            await _context.Roles.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                request.SortColumn, request.SortOrder,
                                                                request.PageNumber, request.PageSize, cancellationToken);
    }
}
