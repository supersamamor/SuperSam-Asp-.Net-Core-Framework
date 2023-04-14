using AutoMapper;
using CelerSoft.Common.Core.Mapping;
using CelerSoft.TurboERP.Application.Features.TurboERP.Approval.Commands;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Item.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Brand.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Product.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierContactPerson.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Order.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Commands;



namespace CelerSoft.TurboERP.Application.Features.TurboERP;

public class TurboERPProfile : Profile
{
    public TurboERPProfile()
    {
        CreateMap<AddUnitCommand, UnitState>();
		CreateMap <EditUnitCommand, UnitState>().IgnoreBaseEntityProperties();
		CreateMap<AddItemTypeCommand, ItemTypeState>();
		CreateMap <EditItemTypeCommand, ItemTypeState>().IgnoreBaseEntityProperties();
		CreateMap<AddItemCommand, ItemState>();
		CreateMap <EditItemCommand, ItemState>().IgnoreBaseEntityProperties();
		CreateMap<AddBrandCommand, BrandState>();
		CreateMap <EditBrandCommand, BrandState>().IgnoreBaseEntityProperties();
		CreateMap<AddProductCommand, ProductState>();
		CreateMap <EditProductCommand, ProductState>().IgnoreBaseEntityProperties();
		CreateMap<AddProductImageCommand, ProductImageState>();
		CreateMap <EditProductImageCommand, ProductImageState>().IgnoreBaseEntityProperties();
		CreateMap<AddCustomerCommand, CustomerState>();
		CreateMap <EditCustomerCommand, CustomerState>().IgnoreBaseEntityProperties();
		CreateMap<AddSupplierCommand, SupplierState>();
		CreateMap <EditSupplierCommand, SupplierState>().IgnoreBaseEntityProperties();
		CreateMap<AddCustomerContactPersonCommand, CustomerContactPersonState>();
		CreateMap <EditCustomerContactPersonCommand, CustomerContactPersonState>().IgnoreBaseEntityProperties();
		CreateMap<AddSupplierContactPersonCommand, SupplierContactPersonState>();
		CreateMap <EditSupplierContactPersonCommand, SupplierContactPersonState>().IgnoreBaseEntityProperties();
		CreateMap<AddPurchaseRequisitionCommand, PurchaseRequisitionState>();
		CreateMap <EditPurchaseRequisitionCommand, PurchaseRequisitionState>().IgnoreBaseEntityProperties();
		CreateMap<AddPurchaseRequisitionItemCommand, PurchaseRequisitionItemState>();
		CreateMap <EditPurchaseRequisitionItemCommand, PurchaseRequisitionItemState>().IgnoreBaseEntityProperties();
		CreateMap<AddSupplierQuotationCommand, SupplierQuotationState>();
		CreateMap <EditSupplierQuotationCommand, SupplierQuotationState>().IgnoreBaseEntityProperties();
		CreateMap<AddSupplierQuotationItemCommand, SupplierQuotationItemState>();
		CreateMap <EditSupplierQuotationItemCommand, SupplierQuotationItemState>().IgnoreBaseEntityProperties();
		CreateMap<AddPurchaseCommand, PurchaseState>();
		CreateMap <EditPurchaseCommand, PurchaseState>().IgnoreBaseEntityProperties();
		CreateMap<AddPurchaseItemCommand, PurchaseItemState>();
		CreateMap <EditPurchaseItemCommand, PurchaseItemState>().IgnoreBaseEntityProperties();
		CreateMap<AddInventoryCommand, InventoryState>();
		CreateMap <EditInventoryCommand, InventoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddInventoryHistoryCommand, InventoryHistoryState>();
		CreateMap <EditInventoryHistoryCommand, InventoryHistoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddShoppingCartCommand, ShoppingCartState>();
		CreateMap <EditShoppingCartCommand, ShoppingCartState>().IgnoreBaseEntityProperties();
		CreateMap<AddOrderCommand, OrderState>();
		CreateMap <EditOrderCommand, OrderState>().IgnoreBaseEntityProperties();
		CreateMap<AddOrderItemCommand, OrderItemState>();
		CreateMap <EditOrderItemCommand, OrderItemState>().IgnoreBaseEntityProperties();
		CreateMap<AddWebContentCommand, WebContentState>();
		CreateMap <EditWebContentCommand, WebContentState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
