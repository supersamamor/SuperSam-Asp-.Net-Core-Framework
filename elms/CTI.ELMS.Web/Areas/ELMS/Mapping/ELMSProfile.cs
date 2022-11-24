using AutoMapper;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Application.Features.ELMS.Approval.Commands;
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
using CTI.ELMS.Application.Features.ELMS.Unit.Queries;

namespace CTI.ELMS.Web.Areas.ELMS.Mapping;

public class ELMSProfile : Profile
{
    public ELMSProfile()
    {
        CreateMap<PPlusConnectionSetupViewModel, AddPPlusConnectionSetupCommand>();
        CreateMap<PPlusConnectionSetupViewModel, EditPPlusConnectionSetupCommand>();
        CreateMap<PPlusConnectionSetupState, PPlusConnectionSetupViewModel>().ReverseMap();
        CreateMap<EntityGroupViewModel, AddEntityGroupCommand>();
        CreateMap<EntityGroupViewModel, EditEntityGroupCommand>();
        CreateMap<EntityGroupState, EntityGroupViewModel>().ForPath(e => e.ForeignKeyPPlusConnectionSetup, o => o.MapFrom(s => s.PPlusConnectionSetup!.PPlusVersionName));
        CreateMap<EntityGroupViewModel, EntityGroupState>();
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
        CreateMap<ProjectState, ProjectViewModel>().ForPath(e => e.ForeignKeyEntityGroup, o => o.MapFrom(s => s.EntityGroup!.Id));
        CreateMap<ProjectViewModel, ProjectState>();
        CreateMap<UserProjectAssignmentViewModel, AddUserProjectAssignmentCommand>();
        CreateMap<UserProjectAssignmentViewModel, EditUserProjectAssignmentCommand>();
        CreateMap<UserProjectAssignmentState, UserProjectAssignmentViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
        CreateMap<UserProjectAssignmentViewModel, UserProjectAssignmentState>();
        CreateMap<UnitViewModel, AddUnitCommand>();
        CreateMap<UnitViewModel, EditUnitCommand>();
        CreateMap<UnitState, UnitViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
        CreateMap<UnitViewModel, UnitState>();
        CreateMap<UnitBudgetViewModel, AddUnitBudgetCommand>();
        CreateMap<UnitBudgetViewModel, EditUnitBudgetCommand>();
        CreateMap<UnitBudgetState, UnitBudgetViewModel>().ForPath(e => e.ForeignKeyUnitBudget, o => o.MapFrom(s => s.UnitBudget!.Id)).ForPath(e => e.ForeignKeyUnit, o => o.MapFrom(s => s.Unit!.Id)).ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
        CreateMap<UnitBudgetViewModel, UnitBudgetState>();
        CreateMap<LeadSourceViewModel, AddLeadSourceCommand>();
        CreateMap<LeadSourceViewModel, EditLeadSourceCommand>();
        CreateMap<LeadSourceState, LeadSourceViewModel>().ReverseMap();
        CreateMap<LeadTouchPointViewModel, AddLeadTouchPointCommand>();
        CreateMap<LeadTouchPointViewModel, EditLeadTouchPointCommand>();
        CreateMap<LeadTouchPointState, LeadTouchPointViewModel>().ReverseMap();
        CreateMap<LeadTaskViewModel, AddLeadTaskCommand>();
        CreateMap<LeadTaskViewModel, EditLeadTaskCommand>();
        CreateMap<LeadTaskState, LeadTaskViewModel>().ReverseMap();
        CreateMap<NextStepViewModel, AddNextStepCommand>();
        CreateMap<NextStepViewModel, EditNextStepCommand>();
        CreateMap<NextStepState, NextStepViewModel>().ReverseMap();
        CreateMap<ClientFeedbackViewModel, AddClientFeedbackCommand>();
        CreateMap<ClientFeedbackViewModel, EditClientFeedbackCommand>();
        CreateMap<ClientFeedbackState, ClientFeedbackViewModel>().ReverseMap();
        CreateMap<LeadTaskClientFeedBackViewModel, AddLeadTaskClientFeedBackCommand>();
        CreateMap<LeadTaskClientFeedBackViewModel, EditLeadTaskClientFeedBackCommand>();
        CreateMap<LeadTaskClientFeedBackState, LeadTaskClientFeedBackViewModel>().ForPath(e => e.ForeignKeyClientFeedback, o => o.MapFrom(s => s.ClientFeedback!.ClientFeedbackName)).ForPath(e => e.ForeignKeyLeadTask, o => o.MapFrom(s => s.LeadTask!.LeadTaskName));
        CreateMap<LeadTaskClientFeedBackViewModel, LeadTaskClientFeedBackState>();
        CreateMap<LeadTaskNextStepViewModel, AddLeadTaskNextStepCommand>();
        CreateMap<LeadTaskNextStepViewModel, EditLeadTaskNextStepCommand>();
        CreateMap<LeadTaskNextStepState, LeadTaskNextStepViewModel>().ForPath(e => e.ForeignKeyLeadTask, o => o.MapFrom(s => s.LeadTask!.LeadTaskName)).ForPath(e => e.ForeignKeyNextStep, o => o.MapFrom(s => s.NextStep!.NextStepTaskName)).ForPath(e => e.ForeignKeyClientFeedback, o => o.MapFrom(s => s.ClientFeedback!.ClientFeedbackName));
        CreateMap<LeadTaskNextStepViewModel, LeadTaskNextStepState>();
        CreateMap<BusinessNatureViewModel, AddBusinessNatureCommand>();
        CreateMap<BusinessNatureViewModel, EditBusinessNatureCommand>();
        CreateMap<BusinessNatureState, BusinessNatureViewModel>().ReverseMap();
        CreateMap<BusinessNatureSubItemViewModel, AddBusinessNatureSubItemCommand>();
        CreateMap<BusinessNatureSubItemViewModel, EditBusinessNatureSubItemCommand>();
        CreateMap<BusinessNatureSubItemState, BusinessNatureSubItemViewModel>().ForPath(e => e.ForeignKeyBusinessNature, o => o.MapFrom(s => s.BusinessNature!.BusinessNatureCode));
        CreateMap<BusinessNatureSubItemViewModel, BusinessNatureSubItemState>();
        CreateMap<BusinessNatureCategoryViewModel, AddBusinessNatureCategoryCommand>();
        CreateMap<BusinessNatureCategoryViewModel, EditBusinessNatureCategoryCommand>();
        CreateMap<BusinessNatureCategoryState, BusinessNatureCategoryViewModel>().ForPath(e => e.ForeignKeyBusinessNatureSubItem, o => o.MapFrom(s => s.BusinessNatureSubItem!.Id));
        CreateMap<BusinessNatureCategoryViewModel, BusinessNatureCategoryState>();
        CreateMap<OperationTypeViewModel, AddOperationTypeCommand>();
        CreateMap<OperationTypeViewModel, EditOperationTypeCommand>();
        CreateMap<OperationTypeState, OperationTypeViewModel>().ReverseMap();
        CreateMap<SalutationViewModel, AddSalutationCommand>();
        CreateMap<SalutationViewModel, EditSalutationCommand>();
        CreateMap<SalutationState, SalutationViewModel>().ReverseMap();
        CreateMap<LeadViewModel, AddLeadCommand>();
        CreateMap<LeadViewModel, EditLeadCommand>();
        CreateMap<LeadState, LeadViewModel>().ForPath(e => e.ForeignKeyBusinessNatureCategory, o => o.MapFrom(s => s.BusinessNatureCategory!.Id)).ForPath(e => e.ForeignKeyBusinessNatureSubItem, o => o.MapFrom(s => s.BusinessNatureSubItem!.Id)).ForPath(e => e.ForeignKeyLeadSource, o => o.MapFrom(s => s.LeadSource!.LeadSourceName)).ForPath(e => e.ForeignKeyBusinessNature, o => o.MapFrom(s => s.BusinessNature!.BusinessNatureCode)).ForPath(e => e.ForeignKeyOperationType, o => o.MapFrom(s => s.OperationType!.OperationTypeName)).ForPath(e => e.ForeignKeyLeadTouchPoint, o => o.MapFrom(s => s.LeadTouchPoint!.LeadTouchPointName));
        CreateMap<LeadViewModel, LeadState>();
        CreateMap<ContactViewModel, AddContactCommand>();
        CreateMap<ContactViewModel, EditContactCommand>();
        CreateMap<ContactState, ContactViewModel>().ForPath(e => e.ForeignKeyLead, o => o.MapFrom(s => s.Lead!.Id));
        CreateMap<ContactViewModel, ContactState>();
        CreateMap<ContactPersonViewModel, AddContactPersonCommand>();
        CreateMap<ContactPersonViewModel, EditContactPersonCommand>();
        CreateMap<ContactPersonState, ContactPersonViewModel>().ForPath(e => e.ForeignKeySalutation, o => o.MapFrom(s => s.Salutation!.SalutationDescription)).ForPath(e => e.ForeignKeyLead, o => o.MapFrom(s => s.Lead!.Id));
        CreateMap<ContactPersonViewModel, ContactPersonState>();
        CreateMap<ActivityViewModel, AddActivityCommand>();
        CreateMap<ActivityViewModel, EditActivityCommand>();
        CreateMap<ActivityState, ActivityViewModel>()
            .ForPath(e => e.ForeignKeyLeadTask, o => o.MapFrom(s => s.LeadTask!.LeadTaskName))
            .ForPath(e => e.ForeignKeyLead, o => o.MapFrom(s => s.Lead!.Id))
            .ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id))
            .ForPath(e => e.ForeignKeyProjectName, o => o.MapFrom(s => s.Project!.ProjectName))
            .ForPath(e => e.ForeignKeyClientFeedback, o => o.MapFrom(s => s.ClientFeedback!.ClientFeedbackName))
            .ForPath(e => e.ForeignKeyNextStep, o => o.MapFrom(s => s.NextStep!.NextStepTaskName));
        CreateMap<ActivityViewModel, ActivityState>();
        CreateMap<ActivityHistoryViewModel, AddActivityHistoryCommand>();
        CreateMap<ActivityHistoryViewModel, EditActivityHistoryCommand>();
        CreateMap<ActivityHistoryState, ActivityHistoryViewModel>().ForPath(e => e.ForeignKeyActivity, o => o.MapFrom(s => s.Activity!.Id)).ForPath(e => e.ForeignKeyLeadTask, o => o.MapFrom(s => s.LeadTask!.LeadTaskName)).ForPath(e => e.ForeignKeyClientFeedback, o => o.MapFrom(s => s.ClientFeedback!.ClientFeedbackName)).ForPath(e => e.ForeignKeyNextStep, o => o.MapFrom(s => s.NextStep!.NextStepTaskName));
        CreateMap<ActivityHistoryViewModel, ActivityHistoryState>();
        CreateMap<UnitActivityViewModel, AddUnitActivityCommand>();
        CreateMap<UnitActivityViewModel, EditUnitActivityCommand>();
        CreateMap<UnitActivityState, UnitActivityViewModel>()
            .ForPath(e => e.ForeignKeyActivity, o => o.MapFrom(s => s.Activity!.Id))
            .ForPath(e => e.ForeignKeyActivityHistory, o => o.MapFrom(s => s.ActivityHistory!.Id))
            .ForPath(e => e.ForeignKeyUnit, o => o.MapFrom(s => s.Unit!.Id))
            .ForPath(e => e.UnitNo, o => o.MapFrom(s => s.Unit!.UnitNo))
            .ForPath(e => e.LotArea, o => o.MapFrom(s => s.Unit!.LotArea))
            .ForPath(e => e.AvailabilityDate, o => o.MapFrom(s => s.Unit!.AvailabilityDate));
        CreateMap<UnitActivityViewModel, UnitActivityState>();
        CreateMap<UnitState, UnitActivityViewModel>()
            .ForMember(e => e.Id, c => c.Ignore())
            .ForMember(e => e.Availability, c => c.Ignore())
            .ForPath(e => e.UnitID, o => o.MapFrom(s => s.Id));      
        CreateMap<OfferingState, OfferingViewModel>()
            .ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id))
            .ForPath(e => e.ForeignKeyProjectName, o => o.MapFrom(s => s.Project!.ProjectName))
            .ForPath(e => e.ForeignKeyLead, o => o.MapFrom(s => s.Lead!.Id));
        CreateMap<OfferingViewModel, OfferingState>();
        CreateMap<OfferingHistoryViewModel, AddOfferingHistoryCommand>();
        CreateMap<OfferingHistoryViewModel, EditOfferingHistoryCommand>();
        CreateMap<OfferingHistoryState, OfferingHistoryViewModel>().ForPath(e => e.ForeignKeyLead, o => o.MapFrom(s => s.Lead!.Id)).ForPath(e => e.ForeignKeyOffering, o => o.MapFrom(s => s.Offering!.Id)).ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
        CreateMap<OfferingHistoryViewModel, OfferingHistoryState>();
        CreateMap<PreSelectedUnitViewModel, AddPreSelectedUnitCommand>();
        CreateMap<PreSelectedUnitViewModel, EditPreSelectedUnitCommand>();
        CreateMap<PreSelectedUnitState, PreSelectedUnitViewModel>()
            .ForPath(e => e.ForeignKeyOffering, o => o.MapFrom(s => s.Offering!.Id))
            .ForPath(e => e.ForeignKeyUnit, o => o.MapFrom(s => s.Unit!.Id))
            .ForPath(e => e.UnitNo, o => o.MapFrom(s => s.Unit!.UnitNo))
            .ForPath(e => e.LotArea, o => o.MapFrom(s => s.Unit!.LotArea))
            .ForPath(e => e.AvailabilityDate, o => o.MapFrom(s => s.Unit!.AvailabilityDate));
        CreateMap<PreSelectedUnitViewModel, PreSelectedUnitState>();
        CreateMap<UnitOfferedViewModel, AddUnitOfferedCommand>();
        CreateMap<UnitOfferedViewModel, EditUnitOfferedCommand>();
        CreateMap<UnitOfferedViewModel, UnitOfferedState>();
        CreateMap<UnitOfferedHistoryViewModel, AddUnitOfferedHistoryCommand>();
        CreateMap<UnitOfferedHistoryViewModel, EditUnitOfferedHistoryCommand>();
        CreateMap<UnitOfferedHistoryState, UnitOfferedHistoryViewModel>().ForPath(e => e.ForeignKeyOfferingHistory, o => o.MapFrom(s => s.OfferingHistory!.Id)).ForPath(e => e.ForeignKeyOffering, o => o.MapFrom(s => s.Offering!.Id)).ForPath(e => e.ForeignKeyUnit, o => o.MapFrom(s => s.Unit!.Id));
        CreateMap<UnitOfferedHistoryViewModel, UnitOfferedHistoryState>();
        CreateMap<UnitGroupViewModel, AddUnitGroupCommand>();
        CreateMap<UnitGroupViewModel, EditUnitGroupCommand>();
        CreateMap<UnitGroupState, UnitGroupViewModel>().ForPath(e => e.ForeignKeyOfferingHistory, o => o.MapFrom(s => s.OfferingHistory!.Id));
        CreateMap<UnitGroupViewModel, UnitGroupState>();
        CreateMap<AnnualIncrementViewModel, AddAnnualIncrementCommand>();
        CreateMap<AnnualIncrementViewModel, EditAnnualIncrementCommand>();
        CreateMap<AnnualIncrementState, AnnualIncrementViewModel>().ForPath(e => e.ForeignKeyUnitOffered, o => o.MapFrom(s => s.UnitOffered!.Id));
        CreateMap<AnnualIncrementViewModel, AnnualIncrementState>();
        CreateMap<AnnualIncrementHistoryViewModel, AddAnnualIncrementHistoryCommand>();
        CreateMap<AnnualIncrementHistoryViewModel, EditAnnualIncrementHistoryCommand>();
        CreateMap<AnnualIncrementHistoryState, AnnualIncrementHistoryViewModel>().ForPath(e => e.ForeignKeyUnitOfferedHistory, o => o.MapFrom(s => s.UnitOfferedHistory!.Id));
        CreateMap<AnnualIncrementHistoryViewModel, AnnualIncrementHistoryState>();
        CreateMap<IFCATransactionTypeViewModel, AddIFCATransactionTypeCommand>();
        CreateMap<IFCATransactionTypeViewModel, EditIFCATransactionTypeCommand>();
        CreateMap<IFCATransactionTypeState, IFCATransactionTypeViewModel>().ForPath(e => e.ForeignKeyEntityGroup, o => o.MapFrom(s => s.EntityGroup!.Id));
        CreateMap<IFCATransactionTypeViewModel, IFCATransactionTypeState>();
        CreateMap<IFCATenantInformationViewModel, AddIFCATenantInformationCommand>();
        CreateMap<IFCATenantInformationViewModel, EditIFCATenantInformationCommand>();
        CreateMap<IFCATenantInformationState, IFCATenantInformationViewModel>().ForPath(e => e.ForeignKeyOffering, o => o.MapFrom(s => s.Offering!.Id)).ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
        CreateMap<IFCATenantInformationViewModel, IFCATenantInformationState>();
        CreateMap<IFCAUnitInformationViewModel, AddIFCAUnitInformationCommand>();
        CreateMap<IFCAUnitInformationViewModel, EditIFCAUnitInformationCommand>();
        CreateMap<IFCAUnitInformationState, IFCAUnitInformationViewModel>().ForPath(e => e.ForeignKeyUnit, o => o.MapFrom(s => s.Unit!.Id)).ForPath(e => e.ForeignKeyIFCATenantInformation, o => o.MapFrom(s => s.IFCATenantInformation!.Id));
        CreateMap<IFCAUnitInformationViewModel, IFCAUnitInformationState>();
        CreateMap<IFCAARLedgerViewModel, AddIFCAARLedgerCommand>();
        CreateMap<IFCAARLedgerViewModel, EditIFCAARLedgerCommand>();
        CreateMap<IFCAARLedgerState, IFCAARLedgerViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
        CreateMap<IFCAARLedgerViewModel, IFCAARLedgerState>();
        CreateMap<IFCAARAllocationViewModel, AddIFCAARAllocationCommand>();
        CreateMap<IFCAARAllocationViewModel, EditIFCAARAllocationCommand>();
        CreateMap<IFCAARAllocationState, IFCAARAllocationViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
        CreateMap<IFCAARAllocationViewModel, IFCAARAllocationState>();
        CreateMap<ReportTableCollectionDetailViewModel, AddReportTableCollectionDetailCommand>();
        CreateMap<ReportTableCollectionDetailViewModel, EditReportTableCollectionDetailCommand>();
        CreateMap<ReportTableCollectionDetailState, ReportTableCollectionDetailViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id)).ForPath(e => e.ForeignKeyIFCATenantInformation, o => o.MapFrom(s => s.IFCATenantInformation!.Id));
        CreateMap<ReportTableCollectionDetailViewModel, ReportTableCollectionDetailState>();
        CreateMap<ReportTableYTDExpirySummaryViewModel, AddReportTableYTDExpirySummaryCommand>();
        CreateMap<ReportTableYTDExpirySummaryViewModel, EditReportTableYTDExpirySummaryCommand>();
        CreateMap<ReportTableYTDExpirySummaryState, ReportTableYTDExpirySummaryViewModel>().ReverseMap();

        CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
        CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
        CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
        CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
        CreateMap<Application.Features.ELMS.TabNavigation.Models.TabNavigationModel, LeadTabNavigationPartial>();
        CreateMap<UnitState, PreSelectedUnitViewModel>()
            .ForMember(e => e.Id, c => c.Ignore())
            .ForMember(e => e.Availability, c => c.Ignore())
            .ForPath(e => e.UnitID, o => o.MapFrom(s => s.Id));
        CreateMap<UnitOfferedState, UnitOfferedViewModel>()
            .ForPath(e => e.ForeignKeyOffering, o => o.MapFrom(s => s.Offering!.Id))
            .ForPath(e => e.ForeignKeyUnit, o => o.MapFrom(s => s.Unit!.Id))
            .ForPath(e => e.UnitNo, o => o.MapFrom(s => s.Unit!.UnitNo))
            .ForPath(e => e.LotArea, o => o.MapFrom(s => s.Unit!.LotArea))
            .ForPath(e => e.AvailabilityDate, o => o.MapFrom(s => s.Unit!.AvailabilityDate));
        CreateMap<UnitState, UnitOfferedViewModel>()
          .ForMember(e => e.Id, c => c.Ignore())
          .ForMember(e => e.Availability, c => c.Ignore())
          .ForPath(e => e.UnitID, o => o.MapFrom(s => s.Id));
        CreateMap<AvailableUnitModel, UnitOfferedViewModel>()
          .ForMember(e => e.Id, c => c.Ignore())
          .ForMember(e => e.Availability, c => c.Ignore());
        CreateMap<OfferingViewModel, AddOfferingCommand>()
            .ForMember(e => e.Status, c => c.Ignore());
        CreateMap<OfferingViewModel, EditOfferingCommand>()
            .ForMember(e => e.Status, c => c.Ignore());
        CreateMap<OfferingViewModel, NewOfferingVersionCommand>()
            .ForMember(e => e.Status, c => c.Ignore());
    }
}
