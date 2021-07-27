using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Inventory.Projects.Queries
{
    public record GetProjectsQuery : BaseQuery, IRequest<PagedListResponse<Project>>
    {
    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PagedListResponse<Project>>
    {
        private readonly ApplicationContext _context;

        public GetProjectsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<Project>> Handle(GetProjectsQuery request, CancellationToken cancellationToken) =>
            await _context.Projects.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                   request.SortColumn, request.SortOrder,
                                                                   request.PageNumber, request.PageSize,
                                                                   cancellationToken);
    }
}