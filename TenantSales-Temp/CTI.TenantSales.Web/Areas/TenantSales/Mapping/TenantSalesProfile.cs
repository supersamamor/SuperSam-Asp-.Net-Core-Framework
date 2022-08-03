using AutoMapper;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Application.Features.TenantSales.Approval.Commands;
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

namespace CTI.TenantSales.Web.Areas.TenantSales.Mapping;

public class TenantSalesProfile : Profile
{
    public TenantSalesProfile()
    {
        CreateMap<DatabaseConnectionSetupViewModel, AddDatabaseConnectionSetupCommand>();
		CreateMap<DatabaseConnectionSetupViewModel, EditDatabaseConnectionSetupCommand>();
		CreateMap<DatabaseConnectionSetupState, DatabaseConnectionSetupViewModel>().ReverseMap();
		CreateMap<BusinessUnitViewModel, AddBusinessUnitCommand>();
		CreateMap<BusinessUnitViewModel, EditBusinessUnitCommand>();
		CreateMap<BusinessUnitState, BusinessUnitViewModel>().ReverseMap();
		CreateMap<RentalTypeViewModel, AddRentalTypeCommand>();
		CreateMap<RentalTypeViewModel, EditRentalTypeCommand>();
		CreateMap<RentalTypeState, RentalTypeViewModel>().ReverseMap();
		CreateMap<CompanyViewModel, AddCompanyCommand>();
		CreateMap<CompanyViewModel, EditCompanyCommand>();
		CreateMap<CompanyState, CompanyViewModel>().ForPath(e => e.ForeignKeyDatabaseConnectionSetup, o => o.MapFrom(s => s.DatabaseConnectionSetup!.Code));
		CreateMap<CompanyViewModel, CompanyState>();
		CreateMap<ProjectViewModel, AddProjectCommand>();
		CreateMap<ProjectViewModel, EditProjectCommand>();
		CreateMap<ProjectState, ProjectViewModel>().ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.Id));
		CreateMap<ProjectViewModel, ProjectState>();
		CreateMap<ThemeViewModel, AddThemeCommand>();
		CreateMap<ThemeViewModel, EditThemeCommand>();
		CreateMap<ThemeState, ThemeViewModel>().ReverseMap();
		CreateMap<TenantViewModel, AddTenantCommand>();
		CreateMap<TenantViewModel, EditTenantCommand>();
		CreateMap<TenantState, TenantViewModel>().ForPath(e => e.ForeignKeyLevel, o => o.MapFrom(s => s.Level!.Id)).ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id)).ForPath(e => e.ForeignKeyRentalType, o => o.MapFrom(s => s.RentalType!.Name));
		CreateMap<TenantViewModel, TenantState>();
		CreateMap<TenantPOSSalesViewModel, AddTenantPOSSalesCommand>();
		CreateMap<TenantPOSSalesViewModel, EditTenantPOSSalesCommand>();

		CreateMap<TenantPOSSalesState, TenantPOSSalesViewModel>().ForPath(e => e.ForeignKeyTenantPOS, o => o.MapFrom(s => s.TenantPOS!.Id));
		
		CreateMap<TenantPOSSalesViewModel, TenantPOSSalesState>();

		CreateMap<ClassificationViewModel, AddClassificationCommand>();
		CreateMap<ClassificationViewModel, EditClassificationCommand>();
		CreateMap<ClassificationState, ClassificationViewModel>().ForPath(e => e.ForeignKeyTheme, o => o.MapFrom(s => s.Theme!.Code));
		CreateMap<ClassificationViewModel, ClassificationState>();
		CreateMap<CategoryViewModel, AddCategoryCommand>();
		CreateMap<CategoryViewModel, EditCategoryCommand>();
		CreateMap<CategoryState, CategoryViewModel>().ForPath(e => e.ForeignKeyClassification, o => o.MapFrom(s => s.Classification!.Id));
		CreateMap<CategoryViewModel, CategoryState>();
		CreateMap<ProjectBusinessUnitViewModel, AddProjectBusinessUnitCommand>();
		CreateMap<ProjectBusinessUnitViewModel, EditProjectBusinessUnitCommand>();
		CreateMap<ProjectBusinessUnitState, ProjectBusinessUnitViewModel>().ForPath(e => e.ForeignKeyBusinessUnit, o => o.MapFrom(s => s.BusinessUnit!.Name)).ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
		CreateMap<ProjectBusinessUnitViewModel, ProjectBusinessUnitState>();
		CreateMap<TenantLotViewModel, AddTenantLotCommand>();
		CreateMap<TenantLotViewModel, EditTenantLotCommand>();
		CreateMap<TenantLotState, TenantLotViewModel>().ForPath(e => e.ForeignKeyTenant, o => o.MapFrom(s => s.Tenant!.Id));
		CreateMap<TenantLotViewModel, TenantLotState>();
		CreateMap<LevelViewModel, AddLevelCommand>();
		CreateMap<LevelViewModel, EditLevelCommand>();
		CreateMap<LevelState, LevelViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id));
		CreateMap<LevelViewModel, LevelState>();
		CreateMap<SalesCategoryViewModel, AddSalesCategoryCommand>();
		CreateMap<SalesCategoryViewModel, EditSalesCategoryCommand>();
		CreateMap<SalesCategoryState, SalesCategoryViewModel>().ForPath(e => e.ForeignKeyTenant, o => o.MapFrom(s => s.Tenant!.Id));
		CreateMap<SalesCategoryViewModel, SalesCategoryState>();
		CreateMap<TenantContactViewModel, AddTenantContactCommand>();
		CreateMap<TenantContactViewModel, EditTenantContactCommand>();
		CreateMap<TenantContactState, TenantContactViewModel>().ForPath(e => e.ForeignKeyTenant, o => o.MapFrom(s => s.Tenant!.Id));
		CreateMap<TenantContactViewModel, TenantContactState>();
		CreateMap<TenantPOSViewModel, AddTenantPOSCommand>();
		CreateMap<TenantPOSViewModel, EditTenantPOSCommand>();
		CreateMap<TenantPOSState, TenantPOSViewModel>().ForPath(e => e.ForeignKeyTenant, o => o.MapFrom(s => s.Tenant!.Id));
		CreateMap<TenantPOSViewModel, TenantPOSState>();
		CreateMap<RevalidateViewModel, AddRevalidateCommand>();
		CreateMap<RevalidateViewModel, EditRevalidateCommand>();
		CreateMap<RevalidateState, RevalidateViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Id)).ForPath(e => e.ForeignKeyTenant, o => o.MapFrom(s => s.Tenant!.Id));
		CreateMap<RevalidateViewModel, RevalidateState>();

		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();

    }
}
