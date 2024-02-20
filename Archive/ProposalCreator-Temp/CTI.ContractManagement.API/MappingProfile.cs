using AutoMapper;
using CTI.ContractManagement.API.Controllers.v1;
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


namespace CTI.ContractManagement.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationConfigurationViewModel, AddApplicationConfigurationCommand>();
		CreateMap <ApplicationConfigurationViewModel, EditApplicationConfigurationCommand>();
		CreateMap<MilestoneStageViewModel, AddMilestoneStageCommand>();
		CreateMap <MilestoneStageViewModel, EditMilestoneStageCommand>();
		CreateMap<FrequencyViewModel, AddFrequencyCommand>();
		CreateMap <FrequencyViewModel, EditFrequencyCommand>();
		CreateMap<PricingTypeViewModel, AddPricingTypeCommand>();
		CreateMap <PricingTypeViewModel, EditPricingTypeCommand>();
		CreateMap<ProjectCategoryViewModel, AddProjectCategoryCommand>();
		CreateMap <ProjectCategoryViewModel, EditProjectCategoryCommand>();
		CreateMap<DeliverableViewModel, AddDeliverableCommand>();
		CreateMap <DeliverableViewModel, EditDeliverableCommand>();
		CreateMap<ClientViewModel, AddClientCommand>();
		CreateMap <ClientViewModel, EditClientCommand>();
		CreateMap<ProjectViewModel, AddProjectCommand>();
		CreateMap <ProjectViewModel, EditProjectCommand>();
		CreateMap<ProjectDeliverableViewModel, AddProjectDeliverableCommand>();
		CreateMap <ProjectDeliverableViewModel, EditProjectDeliverableCommand>();
		CreateMap<ProjectMilestoneViewModel, AddProjectMilestoneCommand>();
		CreateMap <ProjectMilestoneViewModel, EditProjectMilestoneCommand>();
		CreateMap<ProjectPackageViewModel, AddProjectPackageCommand>();
		CreateMap <ProjectPackageViewModel, EditProjectPackageCommand>();
		CreateMap<ProjectPackageAdditionalDeliverableViewModel, AddProjectPackageAdditionalDeliverableCommand>();
		CreateMap <ProjectPackageAdditionalDeliverableViewModel, EditProjectPackageAdditionalDeliverableCommand>();
		CreateMap<ProjectHistoryViewModel, AddProjectHistoryCommand>();
		CreateMap <ProjectHistoryViewModel, EditProjectHistoryCommand>();
		CreateMap<ProjectDeliverableHistoryViewModel, AddProjectDeliverableHistoryCommand>();
		CreateMap <ProjectDeliverableHistoryViewModel, EditProjectDeliverableHistoryCommand>();
		CreateMap<ProjectMilestoneHistoryViewModel, AddProjectMilestoneHistoryCommand>();
		CreateMap <ProjectMilestoneHistoryViewModel, EditProjectMilestoneHistoryCommand>();
		CreateMap<ProjectPackageHistoryViewModel, AddProjectPackageHistoryCommand>();
		CreateMap <ProjectPackageHistoryViewModel, EditProjectPackageHistoryCommand>();
		CreateMap<ProjectPackageAdditionalDeliverableHistoryViewModel, AddProjectPackageAdditionalDeliverableHistoryCommand>();
		CreateMap <ProjectPackageAdditionalDeliverableHistoryViewModel, EditProjectPackageAdditionalDeliverableHistoryCommand>();
		
    }
}
