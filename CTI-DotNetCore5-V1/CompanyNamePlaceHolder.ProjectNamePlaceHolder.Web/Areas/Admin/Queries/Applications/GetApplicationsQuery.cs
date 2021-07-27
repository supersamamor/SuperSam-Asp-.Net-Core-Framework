using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Applications
{
    public record GetApplicationsQuery : BaseQuery, IRequest<PagedListResponse<OpenIddictEntityFrameworkCoreApplication>>
    {
    }

    public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, PagedListResponse<OpenIddictEntityFrameworkCoreApplication>>
    {
        private readonly IdentityContext _context;

        public GetApplicationsQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<OpenIddictEntityFrameworkCoreApplication>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken) =>
            await _context.Set<OpenIddictEntityFrameworkCoreApplication>()
                          .AsNoTracking()
                          .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn,
                                           request.SortOrder, request.PageNumber, request.PageSize, cancellationToken);
    }
}
