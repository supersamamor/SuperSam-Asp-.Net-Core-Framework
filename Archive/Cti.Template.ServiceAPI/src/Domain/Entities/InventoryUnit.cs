using Cti.Core.Domain.Common;
using Cti.Core.Domain.Common.Contracts;

namespace CHANGE_TO_APP_NAME.Services.Domain.Entities
{
    public class InventoryUnit : AuditableEntity, IAggregateRoot
    {
        public string ReferenceObject { get; set; }
        public string ProjectCode { get; set; }
        public string PhaseBuildingCode { get; set; }
        public string BlockFloorClusterCode { get; set; }
        public string LotUnitShareNumber { get; set; }
        public string InventoryUnitNumber { get; set; }
        public string CompanyCode { get; set; }
        public string UnitClassificationCode { get; set; }
        public string MarketProductTypeCode { get; set; }
        public string MarketProductID { get; set; }
        public string MarketProductUseCode { get; set; }
        public string MarketProductSubTypeCode { get; set; }
        public string MarketProductAttributeCode { get; set; }
        public string ParkingTypeCode { get; set; }
    }
}
