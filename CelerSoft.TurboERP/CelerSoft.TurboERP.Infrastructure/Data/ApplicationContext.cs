using CelerSoft.Common.Data;
using CelerSoft.Common.Identity.Abstractions;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<UnitState> Unit { get; set; } = default!;
	public DbSet<ItemTypeState> ItemType { get; set; } = default!;
	public DbSet<ItemState> Item { get; set; } = default!;
	public DbSet<BrandState> Brand { get; set; } = default!;
	public DbSet<ProductState> Product { get; set; } = default!;
	public DbSet<ProductImageState> ProductImage { get; set; } = default!;
	public DbSet<CustomerState> Customer { get; set; } = default!;
	public DbSet<SupplierState> Supplier { get; set; } = default!;
	public DbSet<CustomerContactPersonState> CustomerContactPerson { get; set; } = default!;
	public DbSet<SupplierContactPersonState> SupplierContactPerson { get; set; } = default!;
	public DbSet<PurchaseRequisitionState> PurchaseRequisition { get; set; } = default!;
	public DbSet<PurchaseRequisitionItemState> PurchaseRequisitionItem { get; set; } = default!;
	public DbSet<SupplierQuotationState> SupplierQuotation { get; set; } = default!;
	public DbSet<SupplierQuotationItemState> SupplierQuotationItem { get; set; } = default!;
	public DbSet<PurchaseState> Purchase { get; set; } = default!;
	public DbSet<PurchaseItemState> PurchaseItem { get; set; } = default!;
	public DbSet<InventoryState> Inventory { get; set; } = default!;
	public DbSet<InventoryHistoryState> InventoryHistory { get; set; } = default!;
	public DbSet<ShoppingCartState> ShoppingCart { get; set; } = default!;
	public DbSet<OrderState> Order { get; set; } = default!;
	public DbSet<OrderItemState> OrderItem { get; set; } = default!;
	public DbSet<WebContentState> WebContent { get; set; } = default!;
	
	public DbSet<ApprovalState> Approval { get; set; } = default!;
	public DbSet<ApproverSetupState> ApproverSetup { get; set; } = default!;
	public DbSet<ApproverAssignmentState> ApproverAssignment { get; set; } = default!;
	public DbSet<ApprovalRecordState> ApprovalRecord { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(t => t.GetProperties())
                                                   .Where(p => p.ClrType == typeof(decimal)
                                                               || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }
        #region Disable Cascade Delete
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
        #endregion
        modelBuilder.Entity<Audit>().Property(e => e.PrimaryKey).HasMaxLength(120);
        modelBuilder.Entity<Audit>().HasIndex(p => p.PrimaryKey);
        // NOTE: DO NOT CREATE EXTENSION METHOD FOR QUERY FILTER!!!
        // It causes filter to be evaluated before user has signed in
        modelBuilder.Entity<UnitState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UnitState>().HasIndex(p => p.Entity);modelBuilder.Entity<UnitState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ItemTypeState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ItemTypeState>().HasIndex(p => p.Entity);modelBuilder.Entity<ItemTypeState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ItemState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ItemState>().HasIndex(p => p.Entity);modelBuilder.Entity<ItemState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<BrandState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<BrandState>().HasIndex(p => p.Entity);modelBuilder.Entity<BrandState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProductState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProductState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProductState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProductImageState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProductImageState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProductImageState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CustomerState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CustomerState>().HasIndex(p => p.Entity);modelBuilder.Entity<CustomerState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SupplierState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SupplierState>().HasIndex(p => p.Entity);modelBuilder.Entity<SupplierState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CustomerContactPersonState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CustomerContactPersonState>().HasIndex(p => p.Entity);modelBuilder.Entity<CustomerContactPersonState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SupplierContactPersonState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SupplierContactPersonState>().HasIndex(p => p.Entity);modelBuilder.Entity<SupplierContactPersonState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PurchaseRequisitionState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PurchaseRequisitionState>().HasIndex(p => p.Entity);modelBuilder.Entity<PurchaseRequisitionState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PurchaseRequisitionItemState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PurchaseRequisitionItemState>().HasIndex(p => p.Entity);modelBuilder.Entity<PurchaseRequisitionItemState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SupplierQuotationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SupplierQuotationState>().HasIndex(p => p.Entity);modelBuilder.Entity<SupplierQuotationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SupplierQuotationItemState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SupplierQuotationItemState>().HasIndex(p => p.Entity);modelBuilder.Entity<SupplierQuotationItemState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PurchaseState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PurchaseState>().HasIndex(p => p.Entity);modelBuilder.Entity<PurchaseState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PurchaseItemState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PurchaseItemState>().HasIndex(p => p.Entity);modelBuilder.Entity<PurchaseItemState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<InventoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<InventoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<InventoryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<InventoryHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<InventoryHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<InventoryHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ShoppingCartState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ShoppingCartState>().HasIndex(p => p.Entity);modelBuilder.Entity<ShoppingCartState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<OrderState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<OrderState>().HasIndex(p => p.Entity);modelBuilder.Entity<OrderState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<OrderItemState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<OrderItemState>().HasIndex(p => p.Entity);modelBuilder.Entity<OrderItemState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<WebContentState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<WebContentState>().HasIndex(p => p.Entity);modelBuilder.Entity<WebContentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<UnitState>().HasIndex(p => p.Abbreviations).IsUnique();
		modelBuilder.Entity<UnitState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<ItemTypeState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<ItemState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<ItemState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<BrandState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<ProductState>().HasIndex(p => p.BarcodeNumber).IsUnique();
		modelBuilder.Entity<CustomerState>().HasIndex(p => p.Company).IsUnique();
		modelBuilder.Entity<CustomerState>().HasIndex(p => p.TINNumber).IsUnique();
		modelBuilder.Entity<SupplierState>().HasIndex(p => p.Company).IsUnique();
		modelBuilder.Entity<SupplierState>().HasIndex(p => p.TINNumber).IsUnique();
		modelBuilder.Entity<PurchaseState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<OrderState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<WebContentState>().HasIndex(p => p.Code).IsUnique();
		
        modelBuilder.Entity<UnitState>().Property(e => e.Abbreviations).HasMaxLength(5);
		modelBuilder.Entity<UnitState>().Property(e => e.Name).HasMaxLength(100);
		modelBuilder.Entity<ItemTypeState>().Property(e => e.Name).HasMaxLength(100);
		modelBuilder.Entity<ItemState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<ItemState>().Property(e => e.Name).HasMaxLength(500);
		modelBuilder.Entity<BrandState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<ProductState>().Property(e => e.Model).HasMaxLength(255);
		modelBuilder.Entity<ProductState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<ProductState>().Property(e => e.BarcodeNumber).HasMaxLength(450);
		modelBuilder.Entity<ProductImageState>().Property(e => e.Path).HasMaxLength(500);
		modelBuilder.Entity<CustomerState>().Property(e => e.Company).HasMaxLength(450);
		modelBuilder.Entity<CustomerState>().Property(e => e.TINNumber).HasMaxLength(20);
		modelBuilder.Entity<CustomerState>().Property(e => e.Address).HasMaxLength(1000);
		modelBuilder.Entity<SupplierState>().Property(e => e.Company).HasMaxLength(450);
		modelBuilder.Entity<SupplierState>().Property(e => e.TINNumber).HasMaxLength(20);
		modelBuilder.Entity<SupplierState>().Property(e => e.Address).HasMaxLength(1000);
		modelBuilder.Entity<CustomerContactPersonState>().Property(e => e.FullName).HasMaxLength(450);
		modelBuilder.Entity<CustomerContactPersonState>().Property(e => e.Position).HasMaxLength(100);
		modelBuilder.Entity<CustomerContactPersonState>().Property(e => e.Email).HasMaxLength(255);
		modelBuilder.Entity<CustomerContactPersonState>().Property(e => e.MobileNumber).HasMaxLength(50);
		modelBuilder.Entity<CustomerContactPersonState>().Property(e => e.PhoneNumber).HasMaxLength(50);
		modelBuilder.Entity<SupplierContactPersonState>().Property(e => e.FullName).HasMaxLength(450);
		modelBuilder.Entity<SupplierContactPersonState>().Property(e => e.Position).HasMaxLength(100);
		modelBuilder.Entity<SupplierContactPersonState>().Property(e => e.Email).HasMaxLength(255);
		modelBuilder.Entity<SupplierContactPersonState>().Property(e => e.MobileNumber).HasMaxLength(50);
		modelBuilder.Entity<SupplierContactPersonState>().Property(e => e.PhoneNumber).HasMaxLength(50);
		modelBuilder.Entity<PurchaseRequisitionState>().Property(e => e.Purpose).HasMaxLength(500);
		modelBuilder.Entity<PurchaseRequisitionState>().Property(e => e.Remarks).HasMaxLength(500);
		modelBuilder.Entity<PurchaseRequisitionState>().Property(e => e.Status).HasMaxLength(50);
		modelBuilder.Entity<PurchaseRequisitionItemState>().Property(e => e.Remarks).HasMaxLength(400);
		modelBuilder.Entity<SupplierQuotationState>().Property(e => e.Canvasser).HasMaxLength(500);
		modelBuilder.Entity<SupplierQuotationState>().Property(e => e.Status).HasMaxLength(50);
		modelBuilder.Entity<SupplierQuotationItemState>().Property(e => e.Remarks).HasMaxLength(400);
		modelBuilder.Entity<PurchaseState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<PurchaseState>().Property(e => e.NotedByUsername).HasMaxLength(400);
		modelBuilder.Entity<PurchaseState>().Property(e => e.ReferenceInvoiceNumber).HasMaxLength(400);
		modelBuilder.Entity<PurchaseItemState>().Property(e => e.Remarks).HasMaxLength(400);
		modelBuilder.Entity<InventoryState>().Property(e => e.DeliveredByFullName).HasMaxLength(255);
		modelBuilder.Entity<InventoryState>().Property(e => e.ReceivedByFullName).HasMaxLength(255);
		modelBuilder.Entity<InventoryState>().Property(e => e.SellByUsername).HasMaxLength(400);
		modelBuilder.Entity<InventoryHistoryState>().Property(e => e.Activity).HasMaxLength(400);
		modelBuilder.Entity<ShoppingCartState>().Property(e => e.ShopperUsername).HasMaxLength(450);
		modelBuilder.Entity<OrderState>().Property(e => e.CheckedByFullName).HasMaxLength(400);
		modelBuilder.Entity<OrderState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<OrderState>().Property(e => e.Remarks).HasMaxLength(500);
		modelBuilder.Entity<OrderState>().Property(e => e.ShopperUsername).HasMaxLength(450);
		modelBuilder.Entity<OrderState>().Property(e => e.Status).HasMaxLength(15);
		modelBuilder.Entity<OrderItemState>().Property(e => e.DeliveredByFullName).HasMaxLength(255);
		modelBuilder.Entity<OrderItemState>().Property(e => e.OrderByUserId).HasMaxLength(400);
		modelBuilder.Entity<OrderItemState>().Property(e => e.ReceivedByFullName).HasMaxLength(255);
		modelBuilder.Entity<OrderItemState>().Property(e => e.Status).HasMaxLength(20);
		modelBuilder.Entity<WebContentState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<WebContentState>().Property(e => e.PageName).HasMaxLength(255);
		modelBuilder.Entity<WebContentState>().Property(e => e.PageTitle).HasMaxLength(255);
		
        modelBuilder.Entity<ItemTypeState>().HasMany(t => t.ItemList).WithOne(l => l.ItemType).HasForeignKey(t => t.ItemTypeId);
		modelBuilder.Entity<UnitState>().HasMany(t => t.ItemList).WithOne(l => l.Unit).HasForeignKey(t => t.UnitId);
		modelBuilder.Entity<ItemState>().HasMany(t => t.ProductList).WithOne(l => l.Item).HasForeignKey(t => t.ItemId);
		modelBuilder.Entity<BrandState>().HasMany(t => t.ProductList).WithOne(l => l.Brand).HasForeignKey(t => t.BrandId);
		modelBuilder.Entity<ProductState>().HasMany(t => t.ProductImageList).WithOne(l => l.Product).HasForeignKey(t => t.ProductId);
		modelBuilder.Entity<CustomerState>().HasMany(t => t.CustomerContactPersonList).WithOne(l => l.Customer).HasForeignKey(t => t.CustomerId);
		modelBuilder.Entity<SupplierState>().HasMany(t => t.SupplierContactPersonList).WithOne(l => l.Supplier).HasForeignKey(t => t.SupplierId);
		modelBuilder.Entity<PurchaseRequisitionState>().HasMany(t => t.PurchaseRequisitionItemList).WithOne(l => l.PurchaseRequisition).HasForeignKey(t => t.PurchaseRequisitionId);
		modelBuilder.Entity<ProductState>().HasMany(t => t.PurchaseRequisitionItemList).WithOne(l => l.Product).HasForeignKey(t => t.ProductId);
		modelBuilder.Entity<PurchaseRequisitionState>().HasMany(t => t.SupplierQuotationList).WithOne(l => l.PurchaseRequisition).HasForeignKey(t => t.PurchaseRequisitionId);
		modelBuilder.Entity<SupplierState>().HasMany(t => t.SupplierQuotationList).WithOne(l => l.Supplier).HasForeignKey(t => t.SupplierId);
		modelBuilder.Entity<SupplierQuotationState>().HasMany(t => t.SupplierQuotationItemList).WithOne(l => l.SupplierQuotation).HasForeignKey(t => t.SupplierQuotationId);
		modelBuilder.Entity<ProductState>().HasMany(t => t.SupplierQuotationItemList).WithOne(l => l.Product).HasForeignKey(t => t.ProductId);
		modelBuilder.Entity<PurchaseRequisitionState>().HasMany(t => t.PurchaseList).WithOne(l => l.PurchaseRequisition).HasForeignKey(t => t.PurchaseRequisitionId);
		modelBuilder.Entity<SupplierQuotationState>().HasMany(t => t.PurchaseList).WithOne(l => l.SupplierQuotation).HasForeignKey(t => t.SupplierQuotationId);
		modelBuilder.Entity<ProductState>().HasMany(t => t.PurchaseItemList).WithOne(l => l.Product).HasForeignKey(t => t.ProductId);
		modelBuilder.Entity<SupplierQuotationItemState>().HasMany(t => t.PurchaseItemList).WithOne(l => l.SupplierQuotationItem).HasForeignKey(t => t.SupplierQuotationItemId);
		modelBuilder.Entity<PurchaseItemState>().HasMany(t => t.InventoryList).WithOne(l => l.PurchaseItem).HasForeignKey(t => t.PurchaseItemId);
		modelBuilder.Entity<ProductState>().HasMany(t => t.InventoryList).WithOne(l => l.Product).HasForeignKey(t => t.ProductId);
		modelBuilder.Entity<InventoryState>().HasMany(t => t.InventoryHistoryList).WithOne(l => l.Inventory).HasForeignKey(t => t.InventoryId);
		modelBuilder.Entity<InventoryState>().HasMany(t => t.ShoppingCartList).WithOne(l => l.Inventory).HasForeignKey(t => t.InventoryId);
		modelBuilder.Entity<CustomerState>().HasMany(t => t.OrderList).WithOne(l => l.Customer).HasForeignKey(t => t.CustomerId);
		modelBuilder.Entity<InventoryState>().HasMany(t => t.OrderItemList).WithOne(l => l.Inventory).HasForeignKey(t => t.InventoryId);
		modelBuilder.Entity<OrderState>().HasMany(t => t.OrderItemList).WithOne(l => l.Order).HasForeignKey(t => t.OrderId);
		
		modelBuilder.Entity<ApprovalRecordState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApprovalState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApproverSetupState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApproverAssignmentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApprovalRecordState>().HasIndex(l => l.DataId);
		modelBuilder.Entity<ApprovalRecordState>().Property(e => e.DataId).HasMaxLength(450);
		modelBuilder.Entity<ApprovalRecordState>().Property(e => e.ApproverSetupId).HasMaxLength(450);
		modelBuilder.Entity<ApprovalRecordState>().HasIndex(l => l.ApproverSetupId);
		modelBuilder.Entity<ApprovalRecordState>().HasIndex(l => l.Status);
		modelBuilder.Entity<ApprovalRecordState>().Property(e => e.Status).HasMaxLength(450);
		modelBuilder.Entity<ApprovalState>().HasIndex(l => l.ApproverUserId);
		modelBuilder.Entity<ApprovalState>().HasIndex(l => l.Status);
		modelBuilder.Entity<ApprovalState>().HasIndex(l => l.EmailSendingStatus);
		modelBuilder.Entity<ApprovalState>().Property(e => e.ApproverUserId).HasMaxLength(450);
		modelBuilder.Entity<ApprovalState>().Property(e => e.Status).HasMaxLength(450);
		modelBuilder.Entity<ApprovalState>().Property(e => e.EmailSendingStatus).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.TableName).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.ApprovalType).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.EmailSubject).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.WorkflowName).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().HasIndex(e => new { e.WorkflowName, e.ApprovalSetupType, e.TableName, e.Entity }).IsUnique();
		modelBuilder.Entity<ApproverAssignmentState>().Property(e => e.ApproverUserId).HasMaxLength(450);
		modelBuilder.Entity<ApproverAssignmentState>().Property(e => e.ApproverRoleId).HasMaxLength(450);
		modelBuilder.Entity<ApproverAssignmentState>().HasIndex(e => new { e.ApproverSetupId, e.ApproverUserId, e.ApproverRoleId }).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
