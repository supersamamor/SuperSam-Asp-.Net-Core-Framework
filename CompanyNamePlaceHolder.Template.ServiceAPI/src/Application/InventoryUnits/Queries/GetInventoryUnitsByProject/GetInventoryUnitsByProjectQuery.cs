using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.InventoryUnits.DTOs;
using ProjectNamePlaceHolder.Services.Domain.Entities;
using Cti.Core.Application.Common.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.InventoryUnits.Queries.GetInventoryUnitsByProject
{
    public class GetInventoryUnitsByProjectQuery : IRequest<IEnumerable<InventoryUnitDto>>
    {
        public string ProjectCode { get; set; } = "0000000001";
    }

    public class GetInventoryUnitsByProjectQueryHandler : IRequestHandler<GetInventoryUnitsByProjectQuery, IEnumerable<InventoryUnitDto>>
    {
        private readonly IRepository<InventoryUnit> _inventoryUnitRepository;

        public GetInventoryUnitsByProjectQueryHandler(IRepository<InventoryUnit> inventoryUnitRepository)
        {
            _inventoryUnitRepository = inventoryUnitRepository;
        }

        public async Task<IEnumerable<InventoryUnitDto>> Handle(GetInventoryUnitsByProjectQuery request, CancellationToken cancellationToken)
        {
            var spec = new InventoryByProjectWithDetailsSpec(request.ProjectCode);
            var unitsDto = await _inventoryUnitRepository.ListAsync(spec);
            return unitsDto;
        }
    }

    public class InventoryByProjectWithDetailsSpec : Specification<InventoryUnit, InventoryUnitDto>
    {
        public InventoryByProjectWithDetailsSpec(string projectCode)
        {
            Query.Where(x => x.ProjectCode == projectCode);
        }
    }
}
