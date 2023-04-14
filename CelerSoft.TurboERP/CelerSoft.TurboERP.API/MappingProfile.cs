using AutoMapper;
using CelerSoft.TurboERP.API.Controllers.v1;
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


namespace CelerSoft.TurboERP.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UnitViewModel, AddUnitCommand>();
		CreateMap <UnitViewModel, EditUnitCommand>();
		CreateMap<ItemTypeViewModel, AddItemTypeCommand>();
		CreateMap <ItemTypeViewModel, EditItemTypeCommand>();
		CreateMap<ItemViewModel, AddItemCommand>();
		CreateMap <ItemViewModel, EditItemCommand>();
		CreateMap<BrandViewModel, AddBrandCommand>();
		CreateMap <BrandViewModel, EditBrandCommand>();
		CreateMap<ProductViewModel, AddProductCommand>();
		CreateMap <ProductViewModel, EditProductCommand>();
		CreateMap<ProductImageViewModel, AddProductImageCommand>();
		CreateMap <ProductImageViewModel, EditProductImageCommand>();
		CreateMap<CustomerViewModel, AddCustomerCommand>();
		CreateMap <CustomerViewModel, EditCustomerCommand>();
		CreateMap<SupplierViewModel, AddSupplierCommand>();
		CreateMap <SupplierViewModel, EditSupplierCommand>();
		CreateMap<CustomerContactPersonViewModel, AddCustomerContactPersonCommand>();
		CreateMap <CustomerContactPersonViewModel, EditCustomerContactPersonCommand>();
		CreateMap<SupplierContactPersonViewModel, AddSupplierContactPersonCommand>();
		CreateMap <SupplierContactPersonViewModel, EditSupplierContactPersonCommand>();
		CreateMap<PurchaseRequisitionViewModel, AddPurchaseRequisitionCommand>();
		CreateMap <PurchaseRequisitionViewModel, EditPurchaseRequisitionCommand>();
		CreateMap<PurchaseRequisitionItemViewModel, AddPurchaseRequisitionItemCommand>();
		CreateMap <PurchaseRequisitionItemViewModel, EditPurchaseRequisitionItemCommand>();
		CreateMap<SupplierQuotationViewModel, AddSupplierQuotationCommand>();
		CreateMap <SupplierQuotationViewModel, EditSupplierQuotationCommand>();
		CreateMap<SupplierQuotationItemViewModel, AddSupplierQuotationItemCommand>();
		CreateMap <SupplierQuotationItemViewModel, EditSupplierQuotationItemCommand>();
		CreateMap<PurchaseViewModel, AddPurchaseCommand>();
		CreateMap <PurchaseViewModel, EditPurchaseCommand>();
		CreateMap<PurchaseItemViewModel, AddPurchaseItemCommand>();
		CreateMap <PurchaseItemViewModel, EditPurchaseItemCommand>();
		CreateMap<InventoryViewModel, AddInventoryCommand>();
		CreateMap <InventoryViewModel, EditInventoryCommand>();
		CreateMap<InventoryHistoryViewModel, AddInventoryHistoryCommand>();
		CreateMap <InventoryHistoryViewModel, EditInventoryHistoryCommand>();
		CreateMap<ShoppingCartViewModel, AddShoppingCartCommand>();
		CreateMap <ShoppingCartViewModel, EditShoppingCartCommand>();
		CreateMap<OrderViewModel, AddOrderCommand>();
		CreateMap <OrderViewModel, EditOrderCommand>();
		CreateMap<OrderItemViewModel, AddOrderItemCommand>();
		CreateMap <OrderItemViewModel, EditOrderItemCommand>();
		CreateMap<WebContentViewModel, AddWebContentCommand>();
		CreateMap <WebContentViewModel, EditWebContentCommand>();
		
    }
}
