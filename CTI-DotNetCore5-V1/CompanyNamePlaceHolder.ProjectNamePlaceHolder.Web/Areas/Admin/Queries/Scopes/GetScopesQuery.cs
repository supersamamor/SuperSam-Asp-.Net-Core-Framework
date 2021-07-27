using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Scopes
{
    public record GetScopesQuery : BaseQuery, IRequest<PagedListResponse<OpenIddictEntityFrameworkCoreScope>>
    {
    }

    public class GetScopesQueryHandler : IRequestHandler<GetScopesQuery, PagedListResponse<OpenIddictEntityFrameworkCoreScope>>
    {
        private readonly IdentityContext _context;

        public GetScopesQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<OpenIddictEntityFrameworkCoreScope>> Handle(GetScopesQuery request, CancellationToken cancellationToken) =>
            await _context.Set<OpenIddictEntityFrameworkCoreScope>()
                          .AsNoTracking()
                          .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn,
                                           request.SortOrder, request.PageNumber, request.PageSize, cancellationToken);
    }
}
