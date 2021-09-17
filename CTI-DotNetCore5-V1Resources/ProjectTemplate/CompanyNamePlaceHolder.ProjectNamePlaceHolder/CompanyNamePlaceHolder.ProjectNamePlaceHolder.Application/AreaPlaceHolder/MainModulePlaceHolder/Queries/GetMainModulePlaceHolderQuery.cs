using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Queries
{
    public record GetMainModulePlaceHolderListQuery : BaseQuery, IRequest<PagedListResponse<Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
    }

    public class GetMainModulePlaceHolderListQueryHandler : IRequestHandler<GetMainModulePlaceHolderListQuery, PagedListResponse<Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        private readonly ApplicationContext _context;

        public GetMainModulePlaceHolderListQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PagedListResponse<Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(GetMainModulePlaceHolderListQuery request, CancellationToken cancellationToken) =>
            await _context.MainModulePlaceHolder.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                   request.SortColumn, request.SortOrder,
                                                                   request.PageNumber, request.PageSize,
                                                                   cancellationToken);
    }
}