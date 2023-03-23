using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.Common.Extensions;
using ProjectNamePlaceHolder.Services.Application.InventoryUnits.DTOs;
using ProjectNamePlaceHolder.Services.Domain.Entities;
using Cti.Core.Application.Common.Models;
using Cti.Core.Application.Common.Persistence;
using Cti.Core.Application.Common.Specification;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.InventoryUnits.Queries.GetInventoryUnitsByProjectPaged
{
    // Define Query Request from PaginatedRequest
    public class GetInventoryUnitsByProjectPagedQuery : PaginatedRequest, IRequest<PaginatedList<InventoryUnitDto>>
    {
        public string ProjectCode { get; set; } = "0000000001";
    }

    public class GetInventoryUnitsByProjectPagedQueryHandler : IRequestHandler<GetInventoryUnitsByProjectPagedQuery, PaginatedList<InventoryUnitDto>>
    {
        private readonly IRepository<InventoryUnit> _inventoryUnitRepository;

        public GetInventoryUnitsByProjectPagedQueryHandler(IRepository<InventoryUnit> inventoryUnitRepository)
        {
            _inventoryUnitRepository = inventoryUnitRepository;
        }

        public async Task<PaginatedList<InventoryUnitDto>> Handle(GetInventoryUnitsByProjectPagedQuery request, CancellationToken cancellationToken)
        {
            // Optional, we'll define fields to be searched if AdvancedSearch.Fields is empty
            if (request.AdvancedSearch is null || request.AdvancedSearch?.Fields.Any() is false)
            {
                if (request.AdvancedSearch == null) request.AdvancedSearch = new Search();
                request.AdvancedSearch.Fields.AddRange(GetInventoryUnitsByProjectPagedSearchFields.SearchFields);
            }

            //Optional, use advance search fields for search keyword
            if (!string.IsNullOrEmpty(request.Keyword) && string.IsNullOrEmpty(request.AdvancedSearch.Keyword))
            {
                request.AdvancedSearch.Keyword = request.Keyword;
                request.Keyword = null;
            }

            var spec = new InventoryByInventoryUnitsByProjectPagedQuerySpec(request);
            var unitsDto = await _inventoryUnitRepository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
            return unitsDto;
        }
    }

    // Define Specification for PaginatedRequest
    public class InventoryByInventoryUnitsByProjectPagedQuerySpec : EntitiesByPaginatedRequestSpec<InventoryUnit, InventoryUnitDto>
    {
        public InventoryByInventoryUnitsByProjectPagedQuerySpec(GetInventoryUnitsByProjectPagedQuery request) : base(request)
        {
            Query.AsNoTracking()
                .Where(x => x.ProjectCode == request.ProjectCode);
        }
    }

    // Define static search fields for effiency
    public static class GetInventoryUnitsByProjectPagedSearchFields
    {
        public static string[] SearchFields => new[] {
                    NameOf<InventoryUnit>.Full(x => x.ReferenceObject),                   
                    NameOf<InventoryUnit>.Full(x => x.LotUnitShareNumber),
                    NameOf<InventoryUnit>.Full(x => x.InventoryUnitNumber),                
                };
    }
}
