using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.ELMS.Application.Features.ELMS.Approval.Commands;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Commands;
using CTI.ELMS.Application.Features.ELMS.EntityGroup.Commands;
using CTI.ELMS.Application.Features.ELMS.Project.Commands;
using CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Commands;
using CTI.ELMS.Application.Features.ELMS.Unit.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitBudget.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadSource.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTask.Commands;
using CTI.ELMS.Application.Features.ELMS.NextStep.Commands;
using CTI.ELMS.Application.Features.ELMS.ClientFeedback.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Commands;
using CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Commands;
using CTI.ELMS.Application.Features.ELMS.BusinessNature.Commands;
using CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Commands;
using CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Commands;
using CTI.ELMS.Application.Features.ELMS.OperationType.Commands;
using CTI.ELMS.Application.Features.ELMS.Salutation.Commands;
using CTI.ELMS.Application.Features.ELMS.Lead.Commands;
using CTI.ELMS.Application.Features.ELMS.Contact.Commands;
using CTI.ELMS.Application.Features.ELMS.ContactPerson.Commands;
using CTI.ELMS.Application.Features.ELMS.Activity.Commands;
using CTI.ELMS.Application.Features.ELMS.ActivityHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitActivity.Commands;
using CTI.ELMS.Application.Features.ELMS.Offering.Commands;
using CTI.ELMS.Application.Features.ELMS.OfferingHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitOffered.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitGroup.Commands;
using CTI.ELMS.Application.Features.ELMS.AnnualIncrement.Commands;
using CTI.ELMS.Application.Features.ELMS.AnnualIncrementHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAARAllocation.Commands;
using CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Commands;
using CTI.ELMS.Application.Features.ELMS.ReportTableYTDExpirySummary.Commands;



namespace CTI.ELMS.Application.Features.ELMS;

public class ELMSProfile : Profile
{
    public ELMSProfile()
    {
        CreateMap<AddPPlusConnectionSetupCommand, PPlusConnectionSetupState>();
        CreateMap<EditPPlusConnectionSetupCommand, PPlusConnectionSetupState>().IgnoreBaseEntityProperties();
        CreateMap<AddEntityGroupCommand, EntityGroupState>();
        CreateMap<EditEntityGroupCommand, EntityGroupState>().IgnoreBaseEntityProperties();
        CreateMap<AddProjectCommand, ProjectState>();
        CreateMap<EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
        CreateMap<AddUserProjectAssignmentCommand, UserProjectAssignmentState>();
        CreateMap<EditUserProjectAssignmentCommand, UserProjectAssignmentState>().IgnoreBaseEntityProperties();
        CreateMap<AddUnitCommand, UnitState>();
        CreateMap<EditUnitCommand, UnitState>().IgnoreBaseEntityProperties();
        CreateMap<AddUnitBudgetCommand, UnitBudgetState>();
        CreateMap<EditUnitBudgetCommand, UnitBudgetState>().IgnoreBaseEntityProperties();
        CreateMap<AddLeadSourceCommand, LeadSourceState>();
        CreateMap<EditLeadSourceCommand, LeadSourceState>().IgnoreBaseEntityProperties();
        CreateMap<AddLeadTouchPointCommand, LeadTouchPointState>();
        CreateMap<EditLeadTouchPointCommand, LeadTouchPointState>().IgnoreBaseEntityProperties();
        CreateMap<AddLeadTaskCommand, LeadTaskState>();
        CreateMap<EditLeadTaskCommand, LeadTaskState>().IgnoreBaseEntityProperties();
        CreateMap<AddNextStepCommand, NextStepState>();
        CreateMap<EditNextStepCommand, NextStepState>().IgnoreBaseEntityProperties();
        CreateMap<AddClientFeedbackCommand, ClientFeedbackState>();
        CreateMap<EditClientFeedbackCommand, ClientFeedbackState>().IgnoreBaseEntityProperties();
        CreateMap<AddLeadTaskClientFeedBackCommand, LeadTaskClientFeedBackState>();
        CreateMap<EditLeadTaskClientFeedBackCommand, LeadTaskClientFeedBackState>().IgnoreBaseEntityProperties();
        CreateMap<AddLeadTaskNextStepCommand, LeadTaskNextStepState>();
        CreateMap<EditLeadTaskNextStepCommand, LeadTaskNextStepState>().IgnoreBaseEntityProperties();
        CreateMap<AddBusinessNatureCommand, BusinessNatureState>();
        CreateMap<EditBusinessNatureCommand, BusinessNatureState>().IgnoreBaseEntityProperties();
        CreateMap<AddBusinessNatureSubItemCommand, BusinessNatureSubItemState>();
        CreateMap<EditBusinessNatureSubItemCommand, BusinessNatureSubItemState>().IgnoreBaseEntityProperties();
        CreateMap<AddBusinessNatureCategoryCommand, BusinessNatureCategoryState>();
        CreateMap<EditBusinessNatureCategoryCommand, BusinessNatureCategoryState>().IgnoreBaseEntityProperties();
        CreateMap<AddOperationTypeCommand, OperationTypeState>();
        CreateMap<EditOperationTypeCommand, OperationTypeState>().IgnoreBaseEntityProperties();
        CreateMap<AddSalutationCommand, SalutationState>();
        CreateMap<EditSalutationCommand, SalutationState>().IgnoreBaseEntityProperties();
        CreateMap<AddLeadCommand, LeadState>();
        CreateMap<EditLeadCommand, LeadState>().IgnoreBaseEntityProperties();
        CreateMap<AddContactCommand, ContactState>();
        CreateMap<EditContactCommand, ContactState>().IgnoreBaseEntityProperties();
        CreateMap<AddContactPersonCommand, ContactPersonState>();
        CreateMap<EditContactPersonCommand, ContactPersonState>().IgnoreBaseEntityProperties();
        CreateMap<EditActivityCommand, ActivityState>().IgnoreBaseEntityProperties();
        CreateMap<AddActivityHistoryCommand, ActivityHistoryState>();
        CreateMap<EditActivityHistoryCommand, ActivityHistoryState>().IgnoreBaseEntityProperties();
        CreateMap<AddUnitActivityCommand, UnitActivityState>();
        CreateMap<EditUnitActivityCommand, UnitActivityState>().IgnoreBaseEntityProperties();
        CreateMap<AddOfferingHistoryCommand, OfferingHistoryState>();
        CreateMap<EditOfferingHistoryCommand, OfferingHistoryState>().IgnoreBaseEntityProperties();
        CreateMap<AddPreSelectedUnitCommand, PreSelectedUnitState>();
        CreateMap<EditPreSelectedUnitCommand, PreSelectedUnitState>().IgnoreBaseEntityProperties();
        CreateMap<AddUnitOfferedCommand, UnitOfferedState>();
        CreateMap<EditUnitOfferedCommand, UnitOfferedState>().IgnoreBaseEntityProperties();
        CreateMap<AddUnitOfferedHistoryCommand, UnitOfferedHistoryState>();
        CreateMap<EditUnitOfferedHistoryCommand, UnitOfferedHistoryState>().IgnoreBaseEntityProperties();
        CreateMap<AddUnitGroupCommand, UnitGroupState>();
        CreateMap<EditUnitGroupCommand, UnitGroupState>().IgnoreBaseEntityProperties();
        CreateMap<AddAnnualIncrementCommand, AnnualIncrementState>();
        CreateMap<EditAnnualIncrementCommand, AnnualIncrementState>().IgnoreBaseEntityProperties();
        CreateMap<AddAnnualIncrementHistoryCommand, AnnualIncrementHistoryState>();
        CreateMap<EditAnnualIncrementHistoryCommand, AnnualIncrementHistoryState>().IgnoreBaseEntityProperties();
        CreateMap<AddIFCATransactionTypeCommand, IFCATransactionTypeState>();
        CreateMap<EditIFCATransactionTypeCommand, IFCATransactionTypeState>().IgnoreBaseEntityProperties();
        CreateMap<AddIFCATenantInformationCommand, IFCATenantInformationState>();
        CreateMap<EditIFCATenantInformationCommand, IFCATenantInformationState>().IgnoreBaseEntityProperties();
        CreateMap<AddIFCAUnitInformationCommand, IFCAUnitInformationState>();
        CreateMap<EditIFCAUnitInformationCommand, IFCAUnitInformationState>().IgnoreBaseEntityProperties();
        CreateMap<AddIFCAARLedgerCommand, IFCAARLedgerState>();
        CreateMap<EditIFCAARLedgerCommand, IFCAARLedgerState>().IgnoreBaseEntityProperties();
        CreateMap<AddIFCAARAllocationCommand, IFCAARAllocationState>();
        CreateMap<EditIFCAARAllocationCommand, IFCAARAllocationState>().IgnoreBaseEntityProperties();
        CreateMap<AddReportTableCollectionDetailCommand, ReportTableCollectionDetailState>();
        CreateMap<EditReportTableCollectionDetailCommand, ReportTableCollectionDetailState>().IgnoreBaseEntityProperties();
        CreateMap<AddReportTableYTDExpirySummaryCommand, ReportTableYTDExpirySummaryState>();
        CreateMap<EditReportTableYTDExpirySummaryCommand, ReportTableYTDExpirySummaryState>().IgnoreBaseEntityProperties();
        CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
        CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
        CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
        CreateMap<ActivityState, ActivityHistoryState>()
            .ForPath(e => e.ActivityID, o => o.MapFrom(s => s.Id))
            .ForMember(e => e.Id, c => c.Ignore())
            .ForMember(e => e.UnitActivityList, c => c.Ignore());
        CreateMap<AddActivityCommand, ActivityState>()
            .ForMember(e => e.Id, c => c.Ignore())
            .ForMember(e => e.ActivityHistoryList, c => c.Ignore());
        CreateMap<OfferingState, OfferingHistoryState>()
           .ForPath(e => e.OfferingID, o => o.MapFrom(s => s.Id))
           .ForMember(e => e.Id, c => c.Ignore())
           .ForMember(e => e.UnitOfferedHistoryList, c => c.Ignore());
        CreateMap<AddOfferingCommand, OfferingState>()
         .ForMember(e => e.Id, c => c.Ignore())
         .ForMember(e => e.OfferingHistoryList, c => c.Ignore());
        CreateMap<UnitOfferedState, UnitOfferedHistoryState>()
          .ForPath(e => e.AnnualIncrementHistoryList, o => o.MapFrom(s => s.AnnualIncrementList))
          .ForMember(e => e.Id, c => c.Ignore());
        CreateMap<AnnualIncrementState, AnnualIncrementHistoryState>()
          .ForMember(e => e.Id, c => c.Ignore());
        CreateMap<EditOfferingCommand, OfferingState>()
            .ForMember(e => e.UnitOfferedList, c => c.Ignore())
            .ForMember(e => e.PreSelectedUnitList, c => c.Ignore())
            .ForMember(e => e.OfferingHistoryList, c => c.Ignore())
            .ForMember(e => e.OfferSheetId, c => c.Ignore())
            .ForMember(e => e.Status, c => c.Ignore())
            .ForMember(e => e.ProjectID, c => c.Ignore())
            .ForMember(e => e.OfferingHistoryID, c => c.Ignore())
            .ForMember(e => e.ANType, c => c.Ignore())
            .ForMember(e => e.TenantContractNo, c => c.Ignore())
            .ForMember(e => e.DocStamp, c => c.Ignore())
            .ForMember(e => e.AwardNoticeCreatedDate, c => c.Ignore())
            .ForMember(e => e.AwardNoticeCreatedBy, c => c.Ignore())
            .ForMember(e => e.SignedOfferSheetDate, c => c.Ignore())
            .ForMember(e => e.TagSignedOfferSheetBy, c => c.Ignore())
            .ForMember(e => e.IsPOS, c => c.Ignore())
            .ForMember(e => e.OfferSheetPerProjectCounter, c => c.Ignore())
            .ForMember(e => e.SignedAwardNoticeDate, c => c.Ignore())
            .ForMember(e => e.TagSignedAwardNoticeBy, c => c.Ignore())
            .ForMember(e => e.BookingDate, c => c.Ignore())
            .ForMember(e => e.SignatoryName, c => c.Ignore())
            .ForMember(e => e.SignatoryPosition, c => c.Ignore())
            .ForMember(e => e.ANSignatoryName, c => c.Ignore())
            .ForMember(e => e.ANSignatoryPosition, c => c.Ignore())
            .ForMember(e => e.LeaseContractCreatedDate, c => c.Ignore())
            .ForMember(e => e.LeaseContractCreatedBy, c => c.Ignore())
            .ForMember(e => e.TagSignedLeaseContractBy, c => c.Ignore())
            .ForMember(e => e.SignedLeaseContractDate, c => c.Ignore())
            .ForMember(e => e.TagForReviewLeaseContractBy, c => c.Ignore())
            .ForMember(e => e.ForReviewLeaseContractDate, c => c.Ignore())
            .ForMember(e => e.TagForFinalPrintLeaseContractBy, c => c.Ignore())
            .ForMember(e => e.ForFinalPrintLeaseContractDate, c => c.Ignore())
            .ForMember(e => e.LeaseContractStatus, c => c.Ignore())
            .ForMember(e => e.ANTermType, c => c.Ignore())
            .ForMember(e => e.ContractTypeID, c => c.Ignore())
            .ForMember(e => e.WitnessName, c => c.Ignore())
            .ForMember(e => e.ModifiedCategory, c => c.Ignore())
            .ForMember(e => e.IsDisabledModifiedCategory, c => c.Ignore())
            .ForMember(e => e.ContractNumber, c => c.Ignore())
            .ForMember(e => e.TotalUnitArea, c => c.Ignore())
            .ForMember(e => e.Location, c => c.Ignore())
            .ForMember(e => e.UnitType, c => c.Ignore())
            .ForMember(e => e.TotalBasicFixedMonthlyRent, c => c.Ignore())
            .ForMember(e => e.TotalMinimumMonthlyRent, c => c.Ignore())
            .ForMember(e => e.TotalLotBudget, c => c.Ignore())
            .ForMember(e => e.TotalPercentageRent, c => c.Ignore())
            .ForMember(e => e.UnitsInformation, c => c.Ignore())
            .IgnoreBaseEntityProperties();
    }
}
