namespace CelerSoft.TurboERP.Web;

public static class Permission
{
    public static IEnumerable<string> GenerateAllPermissions()
    {
        return GeneratePermissionsForModule("Admin")
            .Concat(GeneratePermissionsForModule("Entities"))
            .Concat(GeneratePermissionsForModule("Roles"))
            .Concat(GeneratePermissionsForModule("Users"))
            .Concat(GeneratePermissionsForModule("Apis"))
            .Concat(GeneratePermissionsForModule("Applications"))
            .Concat(GeneratePermissionsForModule("AuditTrail"))
            .Concat(GeneratePermissionsForModule("Unit"))
			.Concat(GeneratePermissionsForModule("ItemType"))
			.Concat(GeneratePermissionsForModule("Item"))
			.Concat(GeneratePermissionsForModule("Brand"))
			.Concat(GeneratePermissionsForModule("Product"))
			.Concat(GeneratePermissionsForModule("ProductImage"))
			.Concat(GeneratePermissionsForModule("Customer"))
			.Concat(GeneratePermissionsForModule("Supplier"))
			.Concat(GeneratePermissionsForModule("CustomerContactPerson"))
			.Concat(GeneratePermissionsForModule("SupplierContactPerson"))
			.Concat(GeneratePermissionsForModule("PurchaseRequisition"))
			.Concat(GeneratePermissionsForModule("PurchaseRequisitionItem"))
			.Concat(GeneratePermissionsForModule("SupplierQuotation"))
			.Concat(GeneratePermissionsForModule("SupplierQuotationItem"))
			.Concat(GeneratePermissionsForModule("Purchase"))
			.Concat(GeneratePermissionsForModule("PurchaseItem"))
			.Concat(GeneratePermissionsForModule("Inventory"))
			.Concat(GeneratePermissionsForModule("InventoryHistory"))
			.Concat(GeneratePermissionsForModule("ShoppingCart"))
			.Concat(GeneratePermissionsForModule("Order"))
			.Concat(GeneratePermissionsForModule("OrderItem"))
			.Concat(GeneratePermissionsForModule("WebContent"))
			
			.Concat(GeneratePermissionsForModule("ApproverSetup"));
    }

    public static IEnumerable<string> GeneratePermissionsForModule(string module)
    {
        var permissions = new List<string>()
        {
            $"Permission.{module}.Create",
            $"Permission.{module}.View",
            $"Permission.{module}.Edit",
            $"Permission.{module}.Delete",
			$"Permission.{module}.Approve",
        };
		if (module == "ApproverSetup")
		{
			permissions.Add($"Permission.{module}.PendingApprovals");
		}
		return permissions;
    }

    public static class Admin
    {
        public const string View = "Permission.Admin.View";
        public const string Create = "Permission.Admin.Create";
        public const string Edit = "Permission.Admin.Edit";
        public const string Delete = "Permission.Admin.Delete";
    }

    public static class Entities
    {
        public const string View = "Permission.Entities.View";
        public const string Create = "Permission.Entities.Create";
        public const string Edit = "Permission.Entities.Edit";
        public const string Delete = "Permission.Entities.Delete";
    }

    public static class Roles
    {
        public const string View = "Permission.Roles.View";
        public const string Create = "Permission.Roles.Create";
        public const string Edit = "Permission.Roles.Edit";
        public const string Delete = "Permission.Roles.Delete";
    }

    public static class Users
    {
        public const string View = "Permission.Users.View";
        public const string Create = "Permission.Users.Create";
        public const string Edit = "Permission.Users.Edit";
        public const string Delete = "Permission.Users.Delete";
    }

    public static class Apis
    {
        public const string View = "Permission.Apis.View";
        public const string Create = "Permission.Apis.Create";
        public const string Edit = "Permission.Apis.Edit";
        public const string Delete = "Permission.Apis.Delete";
    }

    public static class Applications
    {
        public const string View = "Permission.Applications.View";
        public const string Create = "Permission.Applications.Create";
        public const string Edit = "Permission.Applications.Edit";
        public const string Delete = "Permission.Applications.Delete";
    }

    public static class AuditTrail
    {
        public const string View = "Permission.AuditTrail.View";
        public const string Create = "Permission.AuditTrail.Create";
        public const string Edit = "Permission.AuditTrail.Edit";
        public const string Delete = "Permission.AuditTrail.Delete";
    }

    public static class Unit
	{
		public const string View = "Permission.Unit.View";
		public const string Create = "Permission.Unit.Create";
		public const string Edit = "Permission.Unit.Edit";
		public const string Delete = "Permission.Unit.Delete";
	}
	public static class ItemType
	{
		public const string View = "Permission.ItemType.View";
		public const string Create = "Permission.ItemType.Create";
		public const string Edit = "Permission.ItemType.Edit";
		public const string Delete = "Permission.ItemType.Delete";
	}
	public static class Item
	{
		public const string View = "Permission.Item.View";
		public const string Create = "Permission.Item.Create";
		public const string Edit = "Permission.Item.Edit";
		public const string Delete = "Permission.Item.Delete";
	}
	public static class Brand
	{
		public const string View = "Permission.Brand.View";
		public const string Create = "Permission.Brand.Create";
		public const string Edit = "Permission.Brand.Edit";
		public const string Delete = "Permission.Brand.Delete";
	}
	public static class Product
	{
		public const string View = "Permission.Product.View";
		public const string Create = "Permission.Product.Create";
		public const string Edit = "Permission.Product.Edit";
		public const string Delete = "Permission.Product.Delete";
	}
	public static class ProductImage
	{
		public const string View = "Permission.ProductImage.View";
		public const string Create = "Permission.ProductImage.Create";
		public const string Edit = "Permission.ProductImage.Edit";
		public const string Delete = "Permission.ProductImage.Delete";
	}
	public static class Customer
	{
		public const string View = "Permission.Customer.View";
		public const string Create = "Permission.Customer.Create";
		public const string Edit = "Permission.Customer.Edit";
		public const string Delete = "Permission.Customer.Delete";
	}
	public static class Supplier
	{
		public const string View = "Permission.Supplier.View";
		public const string Create = "Permission.Supplier.Create";
		public const string Edit = "Permission.Supplier.Edit";
		public const string Delete = "Permission.Supplier.Delete";
	}
	public static class CustomerContactPerson
	{
		public const string View = "Permission.CustomerContactPerson.View";
		public const string Create = "Permission.CustomerContactPerson.Create";
		public const string Edit = "Permission.CustomerContactPerson.Edit";
		public const string Delete = "Permission.CustomerContactPerson.Delete";
	}
	public static class SupplierContactPerson
	{
		public const string View = "Permission.SupplierContactPerson.View";
		public const string Create = "Permission.SupplierContactPerson.Create";
		public const string Edit = "Permission.SupplierContactPerson.Edit";
		public const string Delete = "Permission.SupplierContactPerson.Delete";
	}
	public static class PurchaseRequisition
	{
		public const string View = "Permission.PurchaseRequisition.View";
		public const string Create = "Permission.PurchaseRequisition.Create";
		public const string Edit = "Permission.PurchaseRequisition.Edit";
		public const string Delete = "Permission.PurchaseRequisition.Delete";
		public const string Approve = "Permission.PurchaseRequisition.Approve";
	}
	public static class PurchaseRequisitionItem
	{
		public const string View = "Permission.PurchaseRequisitionItem.View";
		public const string Create = "Permission.PurchaseRequisitionItem.Create";
		public const string Edit = "Permission.PurchaseRequisitionItem.Edit";
		public const string Delete = "Permission.PurchaseRequisitionItem.Delete";
	}
	public static class SupplierQuotation
	{
		public const string View = "Permission.SupplierQuotation.View";
		public const string Create = "Permission.SupplierQuotation.Create";
		public const string Edit = "Permission.SupplierQuotation.Edit";
		public const string Delete = "Permission.SupplierQuotation.Delete";
		public const string Approve = "Permission.SupplierQuotation.Approve";
	}
	public static class SupplierQuotationItem
	{
		public const string View = "Permission.SupplierQuotationItem.View";
		public const string Create = "Permission.SupplierQuotationItem.Create";
		public const string Edit = "Permission.SupplierQuotationItem.Edit";
		public const string Delete = "Permission.SupplierQuotationItem.Delete";
	}
	public static class Purchase
	{
		public const string View = "Permission.Purchase.View";
		public const string Create = "Permission.Purchase.Create";
		public const string Edit = "Permission.Purchase.Edit";
		public const string Delete = "Permission.Purchase.Delete";
	}
	public static class PurchaseItem
	{
		public const string View = "Permission.PurchaseItem.View";
		public const string Create = "Permission.PurchaseItem.Create";
		public const string Edit = "Permission.PurchaseItem.Edit";
		public const string Delete = "Permission.PurchaseItem.Delete";
	}
	public static class Inventory
	{
		public const string View = "Permission.Inventory.View";
		public const string Create = "Permission.Inventory.Create";
		public const string Edit = "Permission.Inventory.Edit";
		public const string Delete = "Permission.Inventory.Delete";
		public const string Approve = "Permission.Inventory.Approve";
	}
	public static class InventoryHistory
	{
		public const string View = "Permission.InventoryHistory.View";
		public const string Create = "Permission.InventoryHistory.Create";
		public const string Edit = "Permission.InventoryHistory.Edit";
		public const string Delete = "Permission.InventoryHistory.Delete";
	}
	public static class ShoppingCart
	{
		public const string View = "Permission.ShoppingCart.View";
		public const string Create = "Permission.ShoppingCart.Create";
		public const string Edit = "Permission.ShoppingCart.Edit";
		public const string Delete = "Permission.ShoppingCart.Delete";
	}
	public static class Order
	{
		public const string View = "Permission.Order.View";
		public const string Create = "Permission.Order.Create";
		public const string Edit = "Permission.Order.Edit";
		public const string Delete = "Permission.Order.Delete";
		public const string Approve = "Permission.Order.Approve";
	}
	public static class OrderItem
	{
		public const string View = "Permission.OrderItem.View";
		public const string Create = "Permission.OrderItem.Create";
		public const string Edit = "Permission.OrderItem.Edit";
		public const string Delete = "Permission.OrderItem.Delete";
	}
	public static class WebContent
	{
		public const string View = "Permission.WebContent.View";
		public const string Create = "Permission.WebContent.Create";
		public const string Edit = "Permission.WebContent.Edit";
		public const string Delete = "Permission.WebContent.Delete";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
