using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.TenantSales.Application.Features.TenantSales.Approval.Commands;
using CTI.TenantSales.Core.TenantSales;
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



namespace CTI.TenantSales.Application.Features.TenantSales;

public class TenantSalesProfile : Profile
{
    public TenantSalesProfile()
    {
        CreateMap<AddDatabaseConnectionSetupCommand, DatabaseConnectionSetupState>();
		CreateMap <EditDatabaseConnectionSetupCommand, DatabaseConnectionSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddBusinessUnitCommand, BusinessUnitState>();
		CreateMap <EditBusinessUnitCommand, BusinessUnitState>().IgnoreBaseEntityProperties();
		CreateMap<AddRentalTypeCommand, RentalTypeState>();
		CreateMap <EditRentalTypeCommand, RentalTypeState>().IgnoreBaseEntityProperties();
		CreateMap<AddCompanyCommand, CompanyState>();
		CreateMap <EditCompanyCommand, CompanyState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectCommand, ProjectState>();
		CreateMap <EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
		CreateMap<AddThemeCommand, ThemeState>();
		CreateMap <EditThemeCommand, ThemeState>().IgnoreBaseEntityProperties();
		CreateMap<AddTenantCommand, TenantState>();
		CreateMap <EditTenantCommand, TenantState>().IgnoreBaseEntityProperties();
		CreateMap<AddTenantPOSSalesCommand, TenantPOSSalesState>();
		CreateMap <EditTenantPOSSalesCommand, TenantPOSSalesState>().IgnoreBaseEntityProperties();
		CreateMap<AddClassificationCommand, ClassificationState>();
		CreateMap <EditClassificationCommand, ClassificationState>().IgnoreBaseEntityProperties();
		CreateMap<AddCategoryCommand, CategoryState>();
		CreateMap <EditCategoryCommand, CategoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectBusinessUnitCommand, ProjectBusinessUnitState>();
		CreateMap <EditProjectBusinessUnitCommand, ProjectBusinessUnitState>().IgnoreBaseEntityProperties();
		CreateMap<AddTenantLotCommand, TenantLotState>();
		CreateMap <EditTenantLotCommand, TenantLotState>().IgnoreBaseEntityProperties();
		CreateMap<AddLevelCommand, LevelState>();
		CreateMap <EditLevelCommand, LevelState>().IgnoreBaseEntityProperties();
		CreateMap<AddSalesCategoryCommand, SalesCategoryState>();
		CreateMap <EditSalesCategoryCommand, SalesCategoryState>().IgnoreBaseEntityProperties();
		CreateMap<AddTenantContactCommand, TenantContactState>();
		CreateMap <EditTenantContactCommand, TenantContactState>().IgnoreBaseEntityProperties();
		CreateMap<AddTenantPOSCommand, TenantPOSState>();
		CreateMap <EditTenantPOSCommand, TenantPOSState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
