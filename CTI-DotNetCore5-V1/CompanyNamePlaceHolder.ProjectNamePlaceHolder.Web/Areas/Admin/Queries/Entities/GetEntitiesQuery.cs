using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities
{
    public record GetEntitiesQuery : BaseQuery, IRequest<PagedListResponse<Entity>>
    {
    }

    public class GetEntitiesQueryHandler : IRequestHandler<GetEntitiesQuery, PagedListResponse<Entity>>
    {
        private readonly IdentityContext _context;

        public GetEntitiesQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<Entity>> Handle(GetEntitiesQuery request, CancellationToken cancellationToken) =>
            await _context.Entities.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                   request.SortColumn, request.SortOrder,
                                                                   request.PageNumber, request.PageSize,
                                                                   cancellationToken);
    }
}
