using AutoMapper;
using CTI.FAS.API.Controllers.v1;
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


namespace CTI.FAS.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DatabaseConnectionSetupViewModel, AddDatabaseConnectionSetupCommand>();
		CreateMap <DatabaseConnectionSetupViewModel, EditDatabaseConnectionSetupCommand>();
		CreateMap<CompanyViewModel, AddCompanyCommand>();
		CreateMap <CompanyViewModel, EditCompanyCommand>();
		CreateMap<ProjectViewModel, AddProjectCommand>();
		CreateMap <ProjectViewModel, EditProjectCommand>();
		CreateMap<TenantViewModel, AddTenantCommand>();
		CreateMap <TenantViewModel, EditTenantCommand>();
		CreateMap<UserEntityViewModel, AddUserEntityCommand>();
		CreateMap <UserEntityViewModel, EditUserEntityCommand>();
		CreateMap<CheckReleaseOptionViewModel, AddCheckReleaseOptionCommand>();
		CreateMap <CheckReleaseOptionViewModel, EditCheckReleaseOptionCommand>();
		CreateMap<BatchViewModel, AddBatchCommand>();
		CreateMap <BatchViewModel, EditBatchCommand>();
		CreateMap<GeneratedViewModel, AddGeneratedCommand>();
		CreateMap <GeneratedViewModel, EditGeneratedCommand>();
		CreateMap<CreditorViewModel, AddCreditorCommand>();
		CreateMap <CreditorViewModel, EditCreditorCommand>();
		CreateMap<CreditorEmailViewModel, AddCreditorEmailCommand>();
		CreateMap <CreditorEmailViewModel, EditCreditorEmailCommand>();
		
    }
}
