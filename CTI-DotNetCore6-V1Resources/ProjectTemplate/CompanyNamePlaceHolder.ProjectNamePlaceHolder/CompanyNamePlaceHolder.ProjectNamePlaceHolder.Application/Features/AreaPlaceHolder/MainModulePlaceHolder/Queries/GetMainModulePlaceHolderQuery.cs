using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries
{
    public record GetMainModulePlaceHolderQuery : BaseQuery, IRequest<PagedListResponse<Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {     
    }

    public class GetMainModulePlaceHolderQueryHandler : BaseAreaPlaceHolderQueryHandler<Core.AreaPlaceHolder.MainModulePlaceHolder, GetMainModulePlaceHolderQuery>, IRequestHandler<GetMainModulePlaceHolderQuery, PagedListResponse<Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        public GetMainModulePlaceHolderQueryHandler(ApplicationContext context) : base(context) { }

        public override async Task<PagedListResponse<Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(GetMainModulePlaceHolderQuery request, CancellationToken cancellationToken) =>
            await _context.MainModulePlaceHolder.AsNoTracking()                              
                                   .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn, request.SortOrder, request.PageNumber, request.PageSize, cancellationToken);
    }
}