using AutoMapper;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.Approval.Commands;
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


namespace CTI.ContractManagement.Web.Areas.ContractManagement.Mapping;

public class ContractManagementProfile : Profile
{
    public ContractManagementProfile()
    {
        CreateMap<ApplicationConfigurationViewModel, AddApplicationConfigurationCommand>().ForPath(e => e.Logo, o => o.MapFrom(s => s.GeneratedLogoPath));
		CreateMap<ApplicationConfigurationViewModel, EditApplicationConfigurationCommand>().ForPath(e => e.Logo, o => o.MapFrom(s => s.GeneratedLogoPath));
		CreateMap<ApplicationConfigurationState, ApplicationConfigurationViewModel>().ReverseMap();
		CreateMap<MilestoneStageViewModel, AddMilestoneStageCommand>();
		CreateMap<MilestoneStageViewModel, EditMilestoneStageCommand>();
		CreateMap<MilestoneStageState, MilestoneStageViewModel>().ReverseMap();
		CreateMap<FrequencyViewModel, AddFrequencyCommand>();
		CreateMap<FrequencyViewModel, EditFrequencyCommand>();
		CreateMap<FrequencyState, FrequencyViewModel>().ReverseMap();
		CreateMap<PricingTypeViewModel, AddPricingTypeCommand>();
		CreateMap<PricingTypeViewModel, EditPricingTypeCommand>();
		CreateMap<PricingTypeState, PricingTypeViewModel>().ReverseMap();
		CreateMap<ProjectCategoryViewModel, AddProjectCategoryCommand>();
		CreateMap<ProjectCategoryViewModel, EditProjectCategoryCommand>();
		CreateMap<ProjectCategoryState, ProjectCategoryViewModel>().ReverseMap();
		CreateMap<DeliverableViewModel, AddDeliverableCommand>();
		CreateMap<DeliverableViewModel, EditDeliverableCommand>();
		CreateMap<DeliverableState, DeliverableViewModel>().ForPath(e => e.ForeignKeyProjectCategory, o => o.MapFrom(s => s.ProjectCategory!.Name));
		CreateMap<DeliverableViewModel, DeliverableState>();
		CreateMap<ClientViewModel, AddClientCommand>();
		CreateMap<ClientViewModel, EditClientCommand>();
		CreateMap<ClientState, ClientViewModel>().ReverseMap();
		CreateMap<ProjectViewModel, AddProjectCommand>().ForPath(e => e.Logo, o => o.MapFrom(s => s.GeneratedLogoPath));
		CreateMap<ProjectViewModel, EditProjectCommand>().ForPath(e => e.Logo, o => o.MapFrom(s => s.GeneratedLogoPath));
		CreateMap<ProjectState, ProjectViewModel>().ForPath(e => e.ForeignKeyClient, o => o.MapFrom(s => s.Client!.ContactPersonName)).ForPath(e => e.ForeignKeyPricingType, o => o.MapFrom(s => s.PricingType!.Name));
		CreateMap<ProjectViewModel, ProjectState>();
		CreateMap<ProjectDeliverableViewModel, AddProjectDeliverableCommand>();
		CreateMap<ProjectDeliverableViewModel, EditProjectDeliverableCommand>();
		CreateMap<ProjectDeliverableState, ProjectDeliverableViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.DocumentCode)).ForPath(e => e.ForeignKeyDeliverable, o => o.MapFrom(s => s.Deliverable!.Name));
		CreateMap<ProjectDeliverableViewModel, ProjectDeliverableState>();
		CreateMap<ProjectMilestoneViewModel, AddProjectMilestoneCommand>();
		CreateMap<ProjectMilestoneViewModel, EditProjectMilestoneCommand>();
		CreateMap<ProjectMilestoneState, ProjectMilestoneViewModel>().ForPath(e => e.ForeignKeyFrequency, o => o.MapFrom(s => s.Frequency!.Name)).ForPath(e => e.ForeignKeyMilestoneStage, o => o.MapFrom(s => s.MilestoneStage!.Name)).ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.DocumentCode));
		CreateMap<ProjectMilestoneViewModel, ProjectMilestoneState>();
		CreateMap<ProjectPackageViewModel, AddProjectPackageCommand>();
		CreateMap<ProjectPackageViewModel, EditProjectPackageCommand>();
		CreateMap<ProjectPackageState, ProjectPackageViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.DocumentCode));
		CreateMap<ProjectPackageViewModel, ProjectPackageState>();
		CreateMap<ProjectPackageAdditionalDeliverableViewModel, AddProjectPackageAdditionalDeliverableCommand>();
		CreateMap<ProjectPackageAdditionalDeliverableViewModel, EditProjectPackageAdditionalDeliverableCommand>();
		CreateMap<ProjectPackageAdditionalDeliverableState, ProjectPackageAdditionalDeliverableViewModel>().ForPath(e => e.ForeignKeyProjectPackage, o => o.MapFrom(s => s.ProjectPackage!.Id)).ForPath(e => e.ForeignKeyDeliverable, o => o.MapFrom(s => s.Deliverable!.Name));
		CreateMap<ProjectPackageAdditionalDeliverableViewModel, ProjectPackageAdditionalDeliverableState>();
		CreateMap<ProjectHistoryViewModel, AddProjectHistoryCommand>().ForPath(e => e.Logo, o => o.MapFrom(s => s.GeneratedLogoPath));
		CreateMap<ProjectHistoryViewModel, EditProjectHistoryCommand>().ForPath(e => e.Logo, o => o.MapFrom(s => s.GeneratedLogoPath));
		CreateMap<ProjectHistoryState, ProjectHistoryViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.DocumentCode)).ForPath(e => e.ForeignKeyClient, o => o.MapFrom(s => s.Client!.ContactPersonName)).ForPath(e => e.ForeignKeyPricingType, o => o.MapFrom(s => s.PricingType!.Name));
		CreateMap<ProjectHistoryViewModel, ProjectHistoryState>();
		CreateMap<ProjectDeliverableHistoryViewModel, AddProjectDeliverableHistoryCommand>();
		CreateMap<ProjectDeliverableHistoryViewModel, EditProjectDeliverableHistoryCommand>();
		CreateMap<ProjectDeliverableHistoryState, ProjectDeliverableHistoryViewModel>().ForPath(e => e.ForeignKeyProjectHistory, o => o.MapFrom(s => s.ProjectHistory!.Id)).ForPath(e => e.ForeignKeyDeliverable, o => o.MapFrom(s => s.Deliverable!.Name));
		CreateMap<ProjectDeliverableHistoryViewModel, ProjectDeliverableHistoryState>();
		CreateMap<ProjectMilestoneHistoryViewModel, AddProjectMilestoneHistoryCommand>();
		CreateMap<ProjectMilestoneHistoryViewModel, EditProjectMilestoneHistoryCommand>();
		CreateMap<ProjectMilestoneHistoryState, ProjectMilestoneHistoryViewModel>().ForPath(e => e.ForeignKeyProjectHistory, o => o.MapFrom(s => s.ProjectHistory!.Id)).ForPath(e => e.ForeignKeyMilestoneStage, o => o.MapFrom(s => s.MilestoneStage!.Name)).ForPath(e => e.ForeignKeyFrequency, o => o.MapFrom(s => s.Frequency!.Name));
		CreateMap<ProjectMilestoneHistoryViewModel, ProjectMilestoneHistoryState>();
		CreateMap<ProjectPackageHistoryViewModel, AddProjectPackageHistoryCommand>();
		CreateMap<ProjectPackageHistoryViewModel, EditProjectPackageHistoryCommand>();
		CreateMap<ProjectPackageHistoryState, ProjectPackageHistoryViewModel>().ForPath(e => e.ForeignKeyProjectHistory, o => o.MapFrom(s => s.ProjectHistory!.Id));
		CreateMap<ProjectPackageHistoryViewModel, ProjectPackageHistoryState>();
		CreateMap<ProjectPackageAdditionalDeliverableHistoryViewModel, AddProjectPackageAdditionalDeliverableHistoryCommand>();
		CreateMap<ProjectPackageAdditionalDeliverableHistoryViewModel, EditProjectPackageAdditionalDeliverableHistoryCommand>();
		CreateMap<ProjectPackageAdditionalDeliverableHistoryState, ProjectPackageAdditionalDeliverableHistoryViewModel>().ForPath(e => e.ForeignKeyProjectPackageHistory, o => o.MapFrom(s => s.ProjectPackageHistory!.Id)).ForPath(e => e.ForeignKeyDeliverable, o => o.MapFrom(s => s.Deliverable!.Name));
		CreateMap<ProjectPackageAdditionalDeliverableHistoryViewModel, ProjectPackageAdditionalDeliverableHistoryState>();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
