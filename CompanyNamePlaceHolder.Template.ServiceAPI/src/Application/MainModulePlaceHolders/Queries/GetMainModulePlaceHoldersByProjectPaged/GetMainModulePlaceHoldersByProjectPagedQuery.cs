using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.Common.Extensions;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.DTOs;
using ProjectNamePlaceHolder.Services.Domain.Entities;
using Cti.Core.Application.Common.Models;
using Cti.Core.Application.Common.Persistence;
using Cti.Core.Application.Common.Specification;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.Queries.GetMainModulePlaceHoldersByProjectPaged
{
    // Define Query Request from PaginatedRequest
    public class GetMainModulePlaceHoldersByProjectPagedQuery : PaginatedRequest, IRequest<PaginatedList<MainModulePlaceHolderDto>>
    {
        public string ProjectCode { get; set; } = "0000000001";
    }

    public class GetMainModulePlaceHoldersByProjectPagedQueryHandler : IRequestHandler<GetMainModulePlaceHoldersByProjectPagedQuery, PaginatedList<MainModulePlaceHolderDto>>
    {
        private readonly IRepository<MainModulePlaceHolder> _mainModulePlaceHolderRepository;

        public GetMainModulePlaceHoldersByProjectPagedQueryHandler(IRepository<MainModulePlaceHolder> mainModulePlaceHolderRepository)
        {
            _mainModulePlaceHolderRepository = mainModulePlaceHolderRepository;
        }

        public async Task<PaginatedList<MainModulePlaceHolderDto>> Handle(GetMainModulePlaceHoldersByProjectPagedQuery request, CancellationToken cancellationToken)
        {
            // Optional, we'll define fields to be searched if AdvancedSearch.Fields is empty
            if (request.AdvancedSearch is null || request.AdvancedSearch?.Fields.Any() is false)
            {
                if (request.AdvancedSearch == null) request.AdvancedSearch = new Search();
                request.AdvancedSearch.Fields.AddRange(GetMainModulePlaceHoldersByProjectPagedSearchFields.SearchFields);
            }

            //Optional, use advance search fields for search keyword
            if (!string.IsNullOrEmpty(request.Keyword) && string.IsNullOrEmpty(request.AdvancedSearch.Keyword))
            {
                request.AdvancedSearch.Keyword = request.Keyword;
                request.Keyword = null;
            }

            var spec = new InventoryByMainModulePlaceHoldersByProjectPagedQuerySpec(request);
            var unitsDto = await _mainModulePlaceHolderRepository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
            return unitsDto;
        }
    }

    // Define Specification for PaginatedRequest
    public class InventoryByMainModulePlaceHoldersByProjectPagedQuerySpec : EntitiesByPaginatedRequestSpec<MainModulePlaceHolder, MainModulePlaceHolderDto>
    {
        public InventoryByMainModulePlaceHoldersByProjectPagedQuerySpec(GetMainModulePlaceHoldersByProjectPagedQuery request) : base(request)
        {
            Query.AsNoTracking()
                .Where(x => x.ProjectCode == request.ProjectCode);
        }
    }

    // Define static search fields for effiency
    public static class GetMainModulePlaceHoldersByProjectPagedSearchFields
    {
        public static string[] SearchFields => new[] {
                    NameOf<MainModulePlaceHolder>.Full(x => x.ReferenceObject),                   
                    NameOf<MainModulePlaceHolder>.Full(x => x.LotUnitShareNumber),
                    NameOf<MainModulePlaceHolder>.Full(x => x.MainModulePlaceHolderNumber),                
                };
    }
}
