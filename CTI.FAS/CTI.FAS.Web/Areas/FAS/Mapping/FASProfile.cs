using AutoMapper;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Application.Features.FAS.Approval.Commands;
using CTI.FAS.Application.Features.FAS.DatabaseConnectionSetup.Commands;
using CTI.FAS.Application.Features.FAS.Company.Commands;
using CTI.FAS.Application.Features.FAS.Project.Commands;
using CTI.FAS.Application.Features.FAS.Tenant.Commands;
using CTI.FAS.Application.Features.FAS.UserEntity.Commands;
using CTI.FAS.Application.Features.FAS.CheckReleaseOption.Commands;
using CTI.FAS.Application.Features.FAS.Batch.Commands;
using CTI.FAS.Application.Features.FAS.Generated.Commands;
using CTI.FAS.Application.Features.FAS.Creditor.Commands;
using CTI.FAS.Application.Features.FAS.CreditorEmail.Commands;


namespace CTI.FAS.Web.Areas.FAS.Mapping;

public class FASProfile : Profile
{
    public FASProfile()
    {
        CreateMap<DatabaseConnectionSetupViewModel, AddDatabaseConnectionSetupCommand>();
		CreateMap<DatabaseConnectionSetupViewModel, EditDatabaseConnectionSetupCommand>();
		CreateMap<DatabaseConnectionSetupState, DatabaseConnectionSetupViewModel>().ReverseMap();
		CreateMap<CompanyViewModel, AddCompanyCommand>().ForPath(e => e.ImageLogo, o => o.MapFrom(s => s.GeneratedImageLogoPath));
		CreateMap<CompanyViewModel, EditCompanyCommand>().ForPath(e => e.ImageLogo, o => o.MapFrom(s => s.GeneratedImageLogoPath));
		CreateMap<CompanyState, CompanyViewModel>().ForPath(e => e.ForeignKeyDatabaseConnectionSetup, o => o.MapFrom(s => s.DatabaseConnectionSetup!.Code));
		CreateMap<CompanyViewModel, CompanyState>();
		CreateMap<ProjectViewModel, AddProjectCommand>();
		CreateMap<ProjectViewModel, EditProjectCommand>();
		CreateMap<ProjectState, ProjectViewModel>().ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.Id));
		CreateMap<ProjectViewModel, ProjectState>();
		CreateMap<TenantViewModel, AddTenantCommand>();
		CreateMap<TenantViewModel, EditTenantCommand>();
		CreateMap<TenantState, TenantViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
		CreateMap<TenantViewModel, TenantState>();
		CreateMap<UserEntityViewModel, AddUserEntityCommand>();
		CreateMap<UserEntityViewModel, EditUserEntityCommand>();
		CreateMap<UserEntityState, UserEntityViewModel>().ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.Id));
		CreateMap<UserEntityViewModel, UserEntityState>();
		CreateMap<CheckReleaseOptionViewModel, AddCheckReleaseOptionCommand>();
		CreateMap<CheckReleaseOptionViewModel, EditCheckReleaseOptionCommand>();
		CreateMap<CheckReleaseOptionState, CheckReleaseOptionViewModel>().ForPath(e => e.ForeignKeyCreditor, o => o.MapFrom(s => s.Creditor!.Id));
		CreateMap<CheckReleaseOptionViewModel, CheckReleaseOptionState>();
		CreateMap<BatchViewModel, AddBatchCommand>();
		CreateMap<BatchViewModel, EditBatchCommand>();
		CreateMap<BatchState, BatchViewModel>().ReverseMap();
		CreateMap<GeneratedViewModel, AddGeneratedCommand>();
		CreateMap<GeneratedViewModel, EditGeneratedCommand>();
		CreateMap<GeneratedState, GeneratedViewModel>().ForPath(e => e.ForeignKeyBatch, o => o.MapFrom(s => s.Batch!.Id)).ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.Id)).ForPath(e => e.ForeignKeyCreditor, o => o.MapFrom(s => s.Creditor!.Id));
		CreateMap<GeneratedViewModel, GeneratedState>();
		CreateMap<CreditorViewModel, AddCreditorCommand>();
		CreateMap<CreditorViewModel, EditCreditorCommand>();
		CreateMap<CreditorState, CreditorViewModel>().ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.Id));
		CreateMap<CreditorViewModel, CreditorState>();
		CreateMap<CreditorEmailViewModel, AddCreditorEmailCommand>();
		CreateMap<CreditorEmailViewModel, EditCreditorEmailCommand>();
		CreateMap<CreditorEmailState, CreditorEmailViewModel>().ForPath(e => e.ForeignKeyCreditor, o => o.MapFrom(s => s.Creditor!.Id));
		CreateMap<CreditorEmailViewModel, CreditorEmailState>();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
