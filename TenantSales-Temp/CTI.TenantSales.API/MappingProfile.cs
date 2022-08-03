using AutoMapper;
using CTI.TenantSales.API.Controllers.v1;
using CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Commands;
using CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Commands;
using CTI.TenantSales.Application.Features.TenantSales.RentalType.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Company.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Project.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Theme.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Tenant.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Classification.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Category.Commands;
using CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantLot.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Level.Commands;
using CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantContact.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOS.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Revalidate.Commands;


namespace CTI.TenantSales.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DatabaseConnectionSetupViewModel, AddDatabaseConnectionSetupCommand>();
		CreateMap <DatabaseConnectionSetupViewModel, EditDatabaseConnectionSetupCommand>();
		CreateMap<BusinessUnitViewModel, AddBusinessUnitCommand>();
		CreateMap <BusinessUnitViewModel, EditBusinessUnitCommand>();
		CreateMap<RentalTypeViewModel, AddRentalTypeCommand>();
		CreateMap <RentalTypeViewModel, EditRentalTypeCommand>();
		CreateMap<CompanyViewModel, AddCompanyCommand>();
		CreateMap <CompanyViewModel, EditCompanyCommand>();
		CreateMap<ProjectViewModel, AddProjectCommand>();
		CreateMap <ProjectViewModel, EditProjectCommand>();
		CreateMap<ThemeViewModel, AddThemeCommand>();
		CreateMap <ThemeViewModel, EditThemeCommand>();
		CreateMap<TenantViewModel, AddTenantCommand>();
		CreateMap <TenantViewModel, EditTenantCommand>();
		CreateMap<TenantPOSSalesViewModel, AddTenantPOSSalesCommand>();
		CreateMap <TenantPOSSalesViewModel, EditTenantPOSSalesCommand>();
		CreateMap<ClassificationViewModel, AddClassificationCommand>();
		CreateMap <ClassificationViewModel, EditClassificationCommand>();
		CreateMap<CategoryViewModel, AddCategoryCommand>();
		CreateMap <CategoryViewModel, EditCategoryCommand>();
		CreateMap<ProjectBusinessUnitViewModel, AddProjectBusinessUnitCommand>();
		CreateMap <ProjectBusinessUnitViewModel, EditProjectBusinessUnitCommand>();
		CreateMap<TenantLotViewModel, AddTenantLotCommand>();
		CreateMap <TenantLotViewModel, EditTenantLotCommand>();
		CreateMap<LevelViewModel, AddLevelCommand>();
		CreateMap <LevelViewModel, EditLevelCommand>();
		CreateMap<SalesCategoryViewModel, AddSalesCategoryCommand>();
		CreateMap <SalesCategoryViewModel, EditSalesCategoryCommand>();
		CreateMap<TenantContactViewModel, AddTenantContactCommand>();
		CreateMap <TenantContactViewModel, EditTenantContactCommand>();
		CreateMap<TenantPOSViewModel, AddTenantPOSCommand>();
		CreateMap <TenantPOSViewModel, EditTenantPOSCommand>();
		CreateMap<RevalidateViewModel, AddRevalidateCommand>();
		CreateMap <RevalidateViewModel, EditRevalidateCommand>();
		
    }
}
