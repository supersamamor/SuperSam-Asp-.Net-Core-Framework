using CelerSoft.Common.Core.Base.Models;


namespace CelerSoft.TurboERP.Core.TurboERP;

public record ProductState : BaseEntity
{
    public string ItemId { get; init; } = "";
    public string? BrandId { get; init; }
    public string? Model { get; init; }
    public string? Description { get; init; }
    public decimal? MinimumQuantity { get; init; }
    public string? BarcodeNumber { get; init; } = Guid.NewGuid().ToString();
    public ItemState? Item { get; init; }
    public BrandState? Brand { get; init; }
    public IList<ProductImageState>? ProductImageList { get; set; }
    public IList<PurchaseRequisitionItemState>? PurchaseRequisitionItemList { get; set; }
    public IList<SupplierQuotationItemState>? SupplierQuotationItemList { get; set; }
    public IList<PurchaseItemState>? PurchaseItemList { get; set; }
    public IList<InventoryState>? InventoryList { get; set; }
    public string ProductName
    {
        get
        {
            return this.Brand?.Name
                + "/" + this.Item?.Name
                + (!string.IsNullOrEmpty(this.Model) ? "/" + this.Model : "")
                + " - " + this.Description;
        }
    }

}
