using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.ELMS.Core.ELMS;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<PPlusConnectionSetupState> PPlusConnectionSetup { get; set; } = default!;
	public DbSet<EntityGroupState> EntityGroup { get; set; } = default!;
	public DbSet<ProjectState> Project { get; set; } = default!;
	public DbSet<UserProjectAssignmentState> UserProjectAssignment { get; set; } = default!;
	public DbSet<UnitState> Unit { get; set; } = default!;
	public DbSet<UnitBudgetState> UnitBudget { get; set; } = default!;
	public DbSet<LeadSourceState> LeadSource { get; set; } = default!;
	public DbSet<LeadTouchPointState> LeadTouchPoint { get; set; } = default!;
	public DbSet<LeadTaskState> LeadTask { get; set; } = default!;
	public DbSet<NextStepState> NextStep { get; set; } = default!;
	public DbSet<ClientFeedbackState> ClientFeedback { get; set; } = default!;
	public DbSet<LeadTaskClientFeedBackState> LeadTaskClientFeedBack { get; set; } = default!;
	public DbSet<LeadTaskNextStepState> LeadTaskNextStep { get; set; } = default!;
	public DbSet<BusinessNatureState> BusinessNature { get; set; } = default!;
	public DbSet<BusinessNatureSubItemState> BusinessNatureSubItem { get; set; } = default!;
	public DbSet<BusinessNatureCategoryState> BusinessNatureCategory { get; set; } = default!;
	public DbSet<OperationTypeState> OperationType { get; set; } = default!;
	public DbSet<SalutationState> Salutation { get; set; } = default!;
	public DbSet<LeadState> Lead { get; set; } = default!;
	public DbSet<ContactState> Contact { get; set; } = default!;
	public DbSet<ContactPersonState> ContactPerson { get; set; } = default!;
	public DbSet<ActivityState> Activity { get; set; } = default!;
	public DbSet<ActivityHistoryState> ActivityHistory { get; set; } = default!;
	public DbSet<UnitActivityState> UnitActivity { get; set; } = default!;
	public DbSet<OfferingState> Offering { get; set; } = default!;
	public DbSet<OfferingHistoryState> OfferingHistory { get; set; } = default!;
	public DbSet<PreSelectedUnitState> PreSelectedUnit { get; set; } = default!;
	public DbSet<UnitOfferedState> UnitOffered { get; set; } = default!;
	public DbSet<UnitOfferedHistoryState> UnitOfferedHistory { get; set; } = default!;
	public DbSet<UnitGroupState> UnitGroup { get; set; } = default!;
	public DbSet<AnnualIncrementState> AnnualIncrement { get; set; } = default!;
	public DbSet<AnnualIncrementHistoryState> AnnualIncrementHistory { get; set; } = default!;
	public DbSet<IFCATransactionTypeState> IFCATransactionType { get; set; } = default!;
	public DbSet<IFCATenantInformationState> IFCATenantInformation { get; set; } = default!;
	public DbSet<IFCAUnitInformationState> IFCAUnitInformation { get; set; } = default!;
	public DbSet<IFCAARLedgerState> IFCAARLedger { get; set; } = default!;
	public DbSet<IFCAARAllocationState> IFCAARAllocation { get; set; } = default!;
	public DbSet<ReportTableCollectionDetailState> ReportTableCollectionDetail { get; set; } = default!;
	public DbSet<ReportTableYTDExpirySummaryState> ReportTableYTDExpirySummary { get; set; } = default!;
	
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
        modelBuilder.Entity<PPlusConnectionSetupState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PPlusConnectionSetupState>().HasIndex(p => p.Entity);modelBuilder.Entity<PPlusConnectionSetupState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<EntityGroupState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<EntityGroupState>().HasIndex(p => p.Entity);modelBuilder.Entity<EntityGroupState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UserProjectAssignmentState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UserProjectAssignmentState>().HasIndex(p => p.Entity);modelBuilder.Entity<UserProjectAssignmentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UnitState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UnitState>().HasIndex(p => p.Entity);modelBuilder.Entity<UnitState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UnitBudgetState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UnitBudgetState>().HasIndex(p => p.Entity);modelBuilder.Entity<UnitBudgetState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LeadSourceState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LeadSourceState>().HasIndex(p => p.Entity);modelBuilder.Entity<LeadSourceState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LeadTouchPointState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LeadTouchPointState>().HasIndex(p => p.Entity);modelBuilder.Entity<LeadTouchPointState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LeadTaskState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LeadTaskState>().HasIndex(p => p.Entity);modelBuilder.Entity<LeadTaskState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<NextStepState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<NextStepState>().HasIndex(p => p.Entity);modelBuilder.Entity<NextStepState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ClientFeedbackState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ClientFeedbackState>().HasIndex(p => p.Entity);modelBuilder.Entity<ClientFeedbackState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LeadTaskClientFeedBackState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LeadTaskClientFeedBackState>().HasIndex(p => p.Entity);modelBuilder.Entity<LeadTaskClientFeedBackState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LeadTaskNextStepState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LeadTaskNextStepState>().HasIndex(p => p.Entity);modelBuilder.Entity<LeadTaskNextStepState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<BusinessNatureState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<BusinessNatureState>().HasIndex(p => p.Entity);modelBuilder.Entity<BusinessNatureState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<BusinessNatureSubItemState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<BusinessNatureSubItemState>().HasIndex(p => p.Entity);modelBuilder.Entity<BusinessNatureSubItemState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<BusinessNatureCategoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<BusinessNatureCategoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<BusinessNatureCategoryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<OperationTypeState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<OperationTypeState>().HasIndex(p => p.Entity);modelBuilder.Entity<OperationTypeState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SalutationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SalutationState>().HasIndex(p => p.Entity);modelBuilder.Entity<SalutationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LeadState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LeadState>().HasIndex(p => p.Entity);modelBuilder.Entity<LeadState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ContactState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ContactState>().HasIndex(p => p.Entity);modelBuilder.Entity<ContactState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ContactPersonState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ContactPersonState>().HasIndex(p => p.Entity);modelBuilder.Entity<ContactPersonState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ActivityState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ActivityState>().HasIndex(p => p.Entity);modelBuilder.Entity<ActivityState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ActivityHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ActivityHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ActivityHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UnitActivityState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UnitActivityState>().HasIndex(p => p.Entity);modelBuilder.Entity<UnitActivityState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<OfferingState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<OfferingState>().HasIndex(p => p.Entity);modelBuilder.Entity<OfferingState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<OfferingHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<OfferingHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<OfferingHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PreSelectedUnitState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PreSelectedUnitState>().HasIndex(p => p.Entity);modelBuilder.Entity<PreSelectedUnitState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UnitOfferedState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UnitOfferedState>().HasIndex(p => p.Entity);modelBuilder.Entity<UnitOfferedState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UnitOfferedHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UnitOfferedHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<UnitOfferedHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UnitGroupState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UnitGroupState>().HasIndex(p => p.Entity);modelBuilder.Entity<UnitGroupState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<AnnualIncrementState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<AnnualIncrementState>().HasIndex(p => p.Entity);modelBuilder.Entity<AnnualIncrementState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<AnnualIncrementHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<AnnualIncrementHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<AnnualIncrementHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<IFCATransactionTypeState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<IFCATransactionTypeState>().HasIndex(p => p.Entity);modelBuilder.Entity<IFCATransactionTypeState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<IFCATenantInformationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<IFCATenantInformationState>().HasIndex(p => p.Entity);modelBuilder.Entity<IFCATenantInformationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<IFCAUnitInformationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<IFCAUnitInformationState>().HasIndex(p => p.Entity);modelBuilder.Entity<IFCAUnitInformationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<IFCAARLedgerState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<IFCAARLedgerState>().HasIndex(p => p.Entity);modelBuilder.Entity<IFCAARLedgerState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<IFCAARAllocationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<IFCAARAllocationState>().HasIndex(p => p.Entity);modelBuilder.Entity<IFCAARAllocationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportTableCollectionDetailState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportTableCollectionDetailState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportTableCollectionDetailState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportTableYTDExpirySummaryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportTableYTDExpirySummaryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportTableYTDExpirySummaryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<PPlusConnectionSetupState>().HasIndex(p => p.PPlusVersionName).IsUnique();
		modelBuilder.Entity<LeadSourceState>().HasIndex(p => p.LeadSourceName).IsUnique();
		modelBuilder.Entity<LeadTouchPointState>().HasIndex(p => p.LeadTouchPointName).IsUnique();
		modelBuilder.Entity<LeadTaskState>().HasIndex(p => p.LeadTaskName).IsUnique();
		modelBuilder.Entity<NextStepState>().HasIndex(p => p.NextStepTaskName).IsUnique();
		modelBuilder.Entity<ClientFeedbackState>().HasIndex(p => p.ClientFeedbackName).IsUnique();
		modelBuilder.Entity<BusinessNatureState>().HasIndex(p => p.BusinessNatureName).IsUnique();
		modelBuilder.Entity<BusinessNatureState>().HasIndex(p => p.BusinessNatureCode).IsUnique();
		modelBuilder.Entity<OperationTypeState>().HasIndex(p => p.OperationTypeName).IsUnique();
		modelBuilder.Entity<SalutationState>().HasIndex(p => p.SalutationDescription).IsUnique();
		
        modelBuilder.Entity<PPlusConnectionSetupState>().Property(e => e.PPlusVersionName).HasMaxLength(100);
		modelBuilder.Entity<PPlusConnectionSetupState>().Property(e => e.TablePrefix).HasMaxLength(255);
		modelBuilder.Entity<PPlusConnectionSetupState>().Property(e => e.ConnectionString).HasMaxLength(1000);
		modelBuilder.Entity<PPlusConnectionSetupState>().Property(e => e.ExhibitThemeCodes).HasMaxLength(255);
		modelBuilder.Entity<EntityGroupState>().Property(e => e.EntityName).HasMaxLength(100);
		modelBuilder.Entity<EntityGroupState>().Property(e => e.PPLUSEntityCode).HasMaxLength(5);
		modelBuilder.Entity<EntityGroupState>().Property(e => e.EntityShortName).HasMaxLength(20);
		modelBuilder.Entity<EntityGroupState>().Property(e => e.TINNo).HasMaxLength(17);
		modelBuilder.Entity<EntityGroupState>().Property(e => e.EntityDescription).HasMaxLength(100);
		modelBuilder.Entity<EntityGroupState>().Property(e => e.EntityAddress).HasMaxLength(255);
		modelBuilder.Entity<EntityGroupState>().Property(e => e.EntityAddress2).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectName).HasMaxLength(100);
		modelBuilder.Entity<ProjectState>().Property(e => e.DatabaseSource).HasMaxLength(30);
		modelBuilder.Entity<ProjectState>().Property(e => e.IFCAProjectCode).HasMaxLength(20);
		modelBuilder.Entity<ProjectState>().Property(e => e.PayableTo).HasMaxLength(100);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectAddress).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.Location).HasMaxLength(100);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectNameANSection).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatoryName).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatoryPosition).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ANSignatoryName).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ANSignatoryPosition).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatory).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryPosition).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryProofOfIdentity).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryWitness).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryWitnessPosition).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectGreetingsSection).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectShortName).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatureUpper).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatureLower).HasMaxLength(255);
		modelBuilder.Entity<UnitState>().Property(e => e.UnitNo).HasMaxLength(20);
		modelBuilder.Entity<UnitState>().Property(e => e.CurrentTenantContractNo).HasMaxLength(50);
		modelBuilder.Entity<LeadSourceState>().Property(e => e.LeadSourceName).HasMaxLength(255);
		modelBuilder.Entity<LeadTouchPointState>().Property(e => e.LeadTouchPointName).HasMaxLength(255);
		modelBuilder.Entity<LeadTaskState>().Property(e => e.LeadTaskName).HasMaxLength(255);
		modelBuilder.Entity<NextStepState>().Property(e => e.NextStepTaskName).HasMaxLength(255);
		modelBuilder.Entity<ClientFeedbackState>().Property(e => e.ClientFeedbackName).HasMaxLength(255);
		modelBuilder.Entity<LeadTaskClientFeedBackState>().Property(e => e.ActivityStatus).HasMaxLength(25);
		modelBuilder.Entity<BusinessNatureState>().Property(e => e.BusinessNatureName).HasMaxLength(255);
		modelBuilder.Entity<BusinessNatureState>().Property(e => e.BusinessNatureCode).HasMaxLength(255);
		modelBuilder.Entity<OperationTypeState>().Property(e => e.OperationTypeName).HasMaxLength(255);
		modelBuilder.Entity<SalutationState>().Property(e => e.SalutationDescription).HasMaxLength(15);
		modelBuilder.Entity<LeadState>().Property(e => e.Brand).HasMaxLength(255);
		modelBuilder.Entity<LeadState>().Property(e => e.Company).HasMaxLength(255);
		modelBuilder.Entity<LeadState>().Property(e => e.Street).HasMaxLength(100);
		modelBuilder.Entity<LeadState>().Property(e => e.City).HasMaxLength(100);
		modelBuilder.Entity<LeadState>().Property(e => e.Province).HasMaxLength(50);
		modelBuilder.Entity<LeadState>().Property(e => e.TINNumber).HasMaxLength(20);
		modelBuilder.Entity<ContactState>().Property(e => e.ContactDetails).HasMaxLength(255);
		modelBuilder.Entity<ContactPersonState>().Property(e => e.FirstName).HasMaxLength(35);
		modelBuilder.Entity<ContactPersonState>().Property(e => e.MiddleName).HasMaxLength(30);
		modelBuilder.Entity<ContactPersonState>().Property(e => e.LastName).HasMaxLength(70);
		modelBuilder.Entity<ContactPersonState>().Property(e => e.Position).HasMaxLength(120);
		modelBuilder.Entity<ActivityState>().Property(e => e.ActivityRemarks).HasMaxLength(500);
		modelBuilder.Entity<ActivityHistoryState>().Property(e => e.ActivityRemarks).HasMaxLength(500);
		modelBuilder.Entity<OfferingState>().Property(e => e.Status).HasMaxLength(50);
		modelBuilder.Entity<OfferingState>().Property(e => e.ANType).HasMaxLength(100);
		modelBuilder.Entity<OfferingState>().Property(e => e.TenantContractNo).HasMaxLength(50);
		modelBuilder.Entity<OfferingState>().Property(e => e.AwardNoticeCreatedBy).HasMaxLength(450);
		modelBuilder.Entity<OfferingState>().Property(e => e.SignatoryName).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.SignatoryPosition).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.ANSignatoryName).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.ANSignatoryPosition).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.LeaseContractCreatedBy).HasMaxLength(450);
		modelBuilder.Entity<OfferingState>().Property(e => e.WitnessName).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.PermittedUse).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.ModifiedCategory).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.ContractNumber).HasMaxLength(255);
		modelBuilder.Entity<OfferingState>().Property(e => e.ANTermType).HasMaxLength(100);
		modelBuilder.Entity<OfferingHistoryState>().Property(e => e.Status).HasMaxLength(35);
		modelBuilder.Entity<OfferingHistoryState>().Property(e => e.ANType).HasMaxLength(100);
		modelBuilder.Entity<UnitGroupState>().Property(e => e.AreaTypeDescription).HasMaxLength(50);
		modelBuilder.Entity<IFCATransactionTypeState>().Property(e => e.TransCode).HasMaxLength(10);
		modelBuilder.Entity<IFCATransactionTypeState>().Property(e => e.TransGroup).HasMaxLength(20);
		modelBuilder.Entity<IFCATransactionTypeState>().Property(e => e.Description).HasMaxLength(500);
		modelBuilder.Entity<IFCATransactionTypeState>().Property(e => e.Mode).HasMaxLength(1);
		modelBuilder.Entity<IFCATenantInformationState>().Property(e => e.TradeName).HasMaxLength(255);
		modelBuilder.Entity<IFCATenantInformationState>().Property(e => e.TINNumber).HasMaxLength(20);
		modelBuilder.Entity<IFCATenantInformationState>().Property(e => e.TenantCategory).HasMaxLength(100);
		modelBuilder.Entity<IFCATenantInformationState>().Property(e => e.TenantClassification).HasMaxLength(100);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.TenantContractNo).HasMaxLength(20);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.DocumentNo).HasMaxLength(20);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.DocumentDescription).HasMaxLength(1000);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.Mode).HasMaxLength(10);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.LedgerDescription).HasMaxLength(1000);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.TransactionType).HasMaxLength(20);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.LotNo).HasMaxLength(255);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.TaxScheme).HasMaxLength(20);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.ReferenceNo).HasMaxLength(255);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.TransactionClass).HasMaxLength(255);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.GLAccount).HasMaxLength(255);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.TradeName).HasMaxLength(500);
		modelBuilder.Entity<IFCAARLedgerState>().Property(e => e.TransactionDesc).HasMaxLength(500);
		modelBuilder.Entity<IFCAARAllocationState>().Property(e => e.TenantContractNo).HasMaxLength(20);
		modelBuilder.Entity<IFCAARAllocationState>().Property(e => e.DocumentNo).HasMaxLength(20);
		modelBuilder.Entity<IFCAARAllocationState>().Property(e => e.TransactionType).HasMaxLength(20);
		modelBuilder.Entity<ReportTableCollectionDetailState>().Property(e => e.Column1).HasMaxLength(50);
		modelBuilder.Entity<ReportTableCollectionDetailState>().Property(e => e.Column2).HasMaxLength(50);
		modelBuilder.Entity<ReportTableCollectionDetailState>().Property(e => e.Column3).HasMaxLength(50);
		modelBuilder.Entity<ReportTableCollectionDetailState>().Property(e => e.Column4).HasMaxLength(50);
		modelBuilder.Entity<ReportTableYTDExpirySummaryState>().Property(e => e.EntityShortName).HasMaxLength(20);
		modelBuilder.Entity<ReportTableYTDExpirySummaryState>().Property(e => e.EntityName).HasMaxLength(100);
		modelBuilder.Entity<ReportTableYTDExpirySummaryState>().Property(e => e.ProjectName).HasMaxLength(100);
		modelBuilder.Entity<ReportTableYTDExpirySummaryState>().Property(e => e.Location).HasMaxLength(100);
		modelBuilder.Entity<ReportTableYTDExpirySummaryState>().Property(e => e.ColumnName).HasMaxLength(20);
		
        modelBuilder.Entity<PPlusConnectionSetupState>().HasMany(t => t.EntityGroupList).WithOne(l => l.PPlusConnectionSetup).HasForeignKey(t => t.PPlusConnectionSetupID);
		modelBuilder.Entity<EntityGroupState>().HasMany(t => t.ProjectList).WithOne(l => l.EntityGroup).HasForeignKey(t => t.EntityGroupId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.UserProjectAssignmentList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.UnitList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.UnitBudgetList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<UnitState>().HasMany(t => t.UnitBudgetList).WithOne(l => l.Unit).HasForeignKey(t => t.UnitID);
		modelBuilder.Entity<UnitBudgetState>().HasMany(t => t.UnitBudgetList).WithOne(l => l.UnitBudget).HasForeignKey(t => t.ParentUnitBudgetID);
		modelBuilder.Entity<LeadTaskState>().HasMany(t => t.LeadTaskClientFeedBackList).WithOne(l => l.LeadTask).HasForeignKey(t => t.LeadTaskId);
		modelBuilder.Entity<ClientFeedbackState>().HasMany(t => t.LeadTaskClientFeedBackList).WithOne(l => l.ClientFeedback).HasForeignKey(t => t.ClientFeedbackId);
		modelBuilder.Entity<LeadTaskState>().HasMany(t => t.LeadTaskNextStepList).WithOne(l => l.LeadTask).HasForeignKey(t => t.LeadTaskId);
		modelBuilder.Entity<ClientFeedbackState>().HasMany(t => t.LeadTaskNextStepList).WithOne(l => l.ClientFeedback).HasForeignKey(t => t.ClientFeedbackId);
		modelBuilder.Entity<NextStepState>().HasMany(t => t.LeadTaskNextStepList).WithOne(l => l.NextStep).HasForeignKey(t => t.NextStepId);
		modelBuilder.Entity<BusinessNatureState>().HasMany(t => t.BusinessNatureSubItemList).WithOne(l => l.BusinessNature).HasForeignKey(t => t.BusinessNatureID);
		modelBuilder.Entity<BusinessNatureSubItemState>().HasMany(t => t.BusinessNatureCategoryList).WithOne(l => l.BusinessNatureSubItem).HasForeignKey(t => t.BusinessNatureSubItemID);
		modelBuilder.Entity<LeadSourceState>().HasMany(t => t.LeadList).WithOne(l => l.LeadSource).HasForeignKey(t => t.LeadSourceId);
		modelBuilder.Entity<LeadTouchPointState>().HasMany(t => t.LeadList).WithOne(l => l.LeadTouchPoint).HasForeignKey(t => t.LeadTouchpointId);
		modelBuilder.Entity<OperationTypeState>().HasMany(t => t.LeadList).WithOne(l => l.OperationType).HasForeignKey(t => t.OperationTypeID);
		modelBuilder.Entity<BusinessNatureState>().HasMany(t => t.LeadList).WithOne(l => l.BusinessNature).HasForeignKey(t => t.BusinessNatureID);
		modelBuilder.Entity<BusinessNatureSubItemState>().HasMany(t => t.LeadList).WithOne(l => l.BusinessNatureSubItem).HasForeignKey(t => t.BusinessNatureSubItemID);
		modelBuilder.Entity<BusinessNatureCategoryState>().HasMany(t => t.LeadList).WithOne(l => l.BusinessNatureCategory).HasForeignKey(t => t.BusinessNatureCategoryID);
		modelBuilder.Entity<LeadState>().HasMany(t => t.ContactList).WithOne(l => l.Lead).HasForeignKey(t => t.LeadID);
		modelBuilder.Entity<LeadState>().HasMany(t => t.ContactPersonList).WithOne(l => l.Lead).HasForeignKey(t => t.LeadId);
		modelBuilder.Entity<SalutationState>().HasMany(t => t.ContactPersonList).WithOne(l => l.Salutation).HasForeignKey(t => t.SalutationID);
		modelBuilder.Entity<LeadState>().HasMany(t => t.ActivityList).WithOne(l => l.Lead).HasForeignKey(t => t.LeadID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.ActivityList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<LeadTaskState>().HasMany(t => t.ActivityList).WithOne(l => l.LeadTask).HasForeignKey(t => t.LeadTaskId);
		modelBuilder.Entity<ClientFeedbackState>().HasMany(t => t.ActivityList).WithOne(l => l.ClientFeedback).HasForeignKey(t => t.ClientFeedbackId);
		modelBuilder.Entity<NextStepState>().HasMany(t => t.ActivityList).WithOne(l => l.NextStep).HasForeignKey(t => t.NextStepId);
		modelBuilder.Entity<ActivityState>().HasMany(t => t.ActivityHistoryList).WithOne(l => l.Activity).HasForeignKey(t => t.ActivityID);
		modelBuilder.Entity<LeadTaskState>().HasMany(t => t.ActivityHistoryList).WithOne(l => l.LeadTask).HasForeignKey(t => t.LeadTaskId);
		modelBuilder.Entity<ClientFeedbackState>().HasMany(t => t.ActivityHistoryList).WithOne(l => l.ClientFeedback).HasForeignKey(t => t.ClientFeedbackId);
		modelBuilder.Entity<NextStepState>().HasMany(t => t.ActivityHistoryList).WithOne(l => l.NextStep).HasForeignKey(t => t.NextStepId);
		modelBuilder.Entity<UnitState>().HasMany(t => t.UnitActivityList).WithOne(l => l.Unit).HasForeignKey(t => t.UnitID);
		modelBuilder.Entity<ActivityHistoryState>().HasMany(t => t.UnitActivityList).WithOne(l => l.ActivityHistory).HasForeignKey(t => t.ActivityHistoryID);
		modelBuilder.Entity<ActivityState>().HasMany(t => t.UnitActivityList).WithOne(l => l.Activity).HasForeignKey(t => t.ActivityID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.OfferingList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<LeadState>().HasMany(t => t.OfferingList).WithOne(l => l.Lead).HasForeignKey(t => t.LeadID);
		modelBuilder.Entity<OfferingState>().HasMany(t => t.OfferingHistoryList).WithOne(l => l.Offering).HasForeignKey(t => t.OfferingID);
		modelBuilder.Entity<LeadState>().HasMany(t => t.OfferingHistoryList).WithOne(l => l.Lead).HasForeignKey(t => t.LeadID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.OfferingHistoryList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<OfferingState>().HasMany(t => t.PreSelectedUnitList).WithOne(l => l.Offering).HasForeignKey(t => t.OfferingID);
		modelBuilder.Entity<UnitState>().HasMany(t => t.PreSelectedUnitList).WithOne(l => l.Unit).HasForeignKey(t => t.UnitID);
		modelBuilder.Entity<OfferingState>().HasMany(t => t.UnitOfferedList).WithOne(l => l.Offering).HasForeignKey(t => t.OfferingID);
		modelBuilder.Entity<UnitState>().HasMany(t => t.UnitOfferedList).WithOne(l => l.Unit).HasForeignKey(t => t.UnitID);
		modelBuilder.Entity<OfferingState>().HasMany(t => t.UnitOfferedHistoryList).WithOne(l => l.Offering).HasForeignKey(t => t.OfferingID);
		modelBuilder.Entity<UnitState>().HasMany(t => t.UnitOfferedHistoryList).WithOne(l => l.Unit).HasForeignKey(t => t.UnitID);
		modelBuilder.Entity<OfferingHistoryState>().HasMany(t => t.UnitOfferedHistoryList).WithOne(l => l.OfferingHistory).HasForeignKey(t => t.OfferingHistoryID);
		modelBuilder.Entity<OfferingHistoryState>().HasMany(t => t.UnitGroupList).WithOne(l => l.OfferingHistory).HasForeignKey(t => t.OfferingHistoryID);
		modelBuilder.Entity<UnitOfferedState>().HasMany(t => t.AnnualIncrementList).WithOne(l => l.UnitOffered).HasForeignKey(t => t.UnitOfferedID);
		modelBuilder.Entity<UnitOfferedHistoryState>().HasMany(t => t.AnnualIncrementHistoryList).WithOne(l => l.UnitOfferedHistory).HasForeignKey(t => t.UnitOfferedHistoryID);
		modelBuilder.Entity<EntityGroupState>().HasMany(t => t.IFCATransactionTypeList).WithOne(l => l.EntityGroup).HasForeignKey(t => t.EntityID);
		modelBuilder.Entity<OfferingState>().HasMany(t => t.IFCATenantInformationList).WithOne(l => l.Offering).HasForeignKey(t => t.OfferingID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.IFCATenantInformationList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<UnitState>().HasMany(t => t.IFCAUnitInformationList).WithOne(l => l.Unit).HasForeignKey(t => t.UnitID);
		modelBuilder.Entity<IFCATenantInformationState>().HasMany(t => t.IFCAUnitInformationList).WithOne(l => l.IFCATenantInformation).HasForeignKey(t => t.IFCATenantInformationID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.IFCAARLedgerList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.IFCAARAllocationList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.ReportTableCollectionDetailList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectID);
		modelBuilder.Entity<IFCATenantInformationState>().HasMany(t => t.ReportTableCollectionDetailList).WithOne(l => l.IFCATenantInformation).HasForeignKey(t => t.IFCATenantInformationID);
		
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
