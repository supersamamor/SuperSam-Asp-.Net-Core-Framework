using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Oidc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Applications
{
    public record GetApplicationsQuery : BaseQuery, IRequest<PagedListResponse<OidcApplication>>
    {
    }

    public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, PagedListResponse<OidcApplication>>
    {
        private readonly IdentityContext _context;

        public GetApplicationsQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<OidcApplication>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken) =>
            await _context.Set<OidcApplication>()
                          .AsNoTracking()
                          .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn,
                                           request.SortOrder, request.PageNumber, request.PageSize, cancellationToken);
    }
}
