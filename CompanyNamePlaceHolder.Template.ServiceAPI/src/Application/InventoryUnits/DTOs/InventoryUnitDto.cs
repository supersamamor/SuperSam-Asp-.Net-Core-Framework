using AutoMapper;
using Cti.Core.Application.Common.Mappings;

namespace ProjectNamePlaceHolder.Services.Application.InventoryUnits.DTOs
{
    public class InventoryUnitDto : IMapFrom<Domain.Entities.InventoryUnit>
    {
        public string ReferenceObject { get; set; }
        public string LotUnitShareNumber { get; set; }
        public string InventoryUnitNumber { get; set; }
    }
}
