using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.Common.Extensions;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.DTOs;
using Cti.Core.Application.Common.Models;
using Cti.Core.Application.Common.Persistence;
using Cti.Core.Application.Common.Specification;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersPaged
{
    // Define Query Request from PaginatedRequest
    public class GetMainModulePlaceHolderPagedQuery : PaginatedRequest, IRequest<PaginatedList<MainModulePlaceHolderDto>>
    { 
    }

    public class GetMainModulePlaceHolderPagedQueryHandler : IRequestHandler<GetMainModulePlaceHolderPagedQuery, PaginatedList<MainModulePlaceHolderDto>>
    {
        private readonly IRepository<Domain.Entities.MainModulePlaceHolder> _mainModulePlaceHolderRepository;

        public GetMainModulePlaceHolderPagedQueryHandler(IRepository<Domain.Entities.MainModulePlaceHolder> mainModulePlaceHolderRepository)
        {
            _mainModulePlaceHolderRepository = mainModulePlaceHolderRepository;
        }

        public async Task<PaginatedList<MainModulePlaceHolderDto>> Handle(GetMainModulePlaceHolderPagedQuery request, CancellationToken cancellationToken)
        {
            // Optional, we'll define fields to be searched if AdvancedSearch.Fields is empty
            if (request.AdvancedSearch is null || request.AdvancedSearch?.Fields.Any() is false)
            {
                if (request.AdvancedSearch == null) request.AdvancedSearch = new Search();
                request.AdvancedSearch.Fields.AddRange(GetMainModulePlaceHolderPagedSearchFields.SearchFields);
            }

            //Optional, use advance search fields for search keyword
            if (!string.IsNullOrEmpty(request.Keyword) && string.IsNullOrEmpty(request.AdvancedSearch.Keyword))
            {
                request.AdvancedSearch.Keyword = request.Keyword;
                request.Keyword = null;
            }

            var spec = new MainModulePlaceHolderPagedQuerySpec(request);
            var unitsDto = await _mainModulePlaceHolderRepository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
            return unitsDto;
        }
    }

    // Define Specification for PaginatedRequest
    public class MainModulePlaceHolderPagedQuerySpec : EntitiesByPaginatedRequestSpec<Domain.Entities.MainModulePlaceHolder, MainModulePlaceHolderDto>
    {
        public MainModulePlaceHolderPagedQuerySpec(GetMainModulePlaceHolderPagedQuery request) : base(request)
        {
            Query.AsNoTracking();
        }
    }

    // Define static search fields for effiency
    public static class GetMainModulePlaceHolderPagedSearchFields
    {
        public static string[] SearchFields => new[] {
                    Template:[InsertNewDynamicSearchFieldTextHere]                
                };
    }
}
