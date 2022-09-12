using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.ContractManagement.Application.Features.ContractManagement.Approval.Commands;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.PricingType.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Client.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Project.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverable.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Commands;



namespace CTI.ContractManagement.Application.Features.ContractManagement;

public class ContractManagementProfile : Profile
{
    public ContractManagementProfile()
    {
        CreateMap<AddApplicationConfigurationCommand, ApplicationConfigurationState>();
		CreateMap <EditApplicationConfigurationCommand, ApplicationConfigurationState>().IgnoreBaseEntityProperties();
		CreateMap<AddMilestoneStageCommand, MilestoneStageState>();
		CreateMap <EditMilestoneStageCommand, MilestoneStageState>().IgnoreBaseEntityProperties();
		CreateMap<AddFrequencyCommand, FrequencyState>();
		CreateMap <EditFrequencyCommand, FrequencyState>().IgnoreBaseEntityProperties();
		CreateMap<AddPricingTypeCommand, PricingTypeState>();
		CreateMap <EditPricingTypeCommand, PricingTypeState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectCategoryCommand, ProjectCategoryState>();
		CreateMap <EditProjectCategoryCommand, ProjectCategoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddDeliverableCommand, DeliverableState>();
		CreateMap <EditDeliverableCommand, DeliverableState>().IgnoreBaseEntityProperties();
		CreateMap<AddClientCommand, ClientState>();
		CreateMap <EditClientCommand, ClientState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectCommand, ProjectState>();
		CreateMap <EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectDeliverableCommand, ProjectDeliverableState>();
		CreateMap <EditProjectDeliverableCommand, ProjectDeliverableState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectMilestoneCommand, ProjectMilestoneState>();
		CreateMap <EditProjectMilestoneCommand, ProjectMilestoneState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectPackageCommand, ProjectPackageState>();
		CreateMap <EditProjectPackageCommand, ProjectPackageState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectPackageAdditionalDeliverableCommand, ProjectPackageAdditionalDeliverableState>();
		CreateMap <EditProjectPackageAdditionalDeliverableCommand, ProjectPackageAdditionalDeliverableState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectHistoryCommand, ProjectHistoryState>();
		CreateMap <EditProjectHistoryCommand, ProjectHistoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectDeliverableHistoryCommand, ProjectDeliverableHistoryState>();
		CreateMap <EditProjectDeliverableHistoryCommand, ProjectDeliverableHistoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectMilestoneHistoryCommand, ProjectMilestoneHistoryState>();
		CreateMap <EditProjectMilestoneHistoryCommand, ProjectMilestoneHistoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectPackageHistoryCommand, ProjectPackageHistoryState>();
		CreateMap <EditProjectPackageHistoryCommand, ProjectPackageHistoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectPackageAdditionalDeliverableHistoryCommand, ProjectPackageAdditionalDeliverableHistoryState>();
		CreateMap <EditProjectPackageAdditionalDeliverableHistoryCommand, ProjectPackageAdditionalDeliverableHistoryState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
