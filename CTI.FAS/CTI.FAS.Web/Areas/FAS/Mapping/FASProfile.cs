using AutoMapper;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Application.Features.FAS.Approval.Commands;
using CTI.FAS.Application.Features.FAS.DatabaseConnectionSetup.Commands;
using CTI.FAS.Application.Features.FAS.Company.Commands;
using CTI.FAS.Application.Features.FAS.Project.Commands;
using CTI.FAS.Application.Features.FAS.Tenant.Commands;
using CTI.FAS.Application.Features.FAS.UserEntity.Commands;
using CTI.FAS.Application.Features.FAS.Batch.Commands;
using CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;
using CTI.FAS.Application.Features.FAS.Creditor.Commands;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
using CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Commands;


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
        CreateMap<CompanyState, CompanyViewModel>().ForPath(e => e.ForeignKeyDatabaseConnectionSetup, o => o.MapFrom(s => s.DatabaseConnectionSetup!.Name));
        CreateMap<CompanyViewModel, CompanyState>();
        CreateMap<ProjectViewModel, AddProjectCommand>();
        CreateMap<ProjectViewModel, EditProjectCommand>();
        CreateMap<ProjectState, ProjectViewModel>().ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.DatabaseConnectionSetup!.Name + " - " + s.Company!.Code + " - " + s.Company!.Name));
        CreateMap<ProjectViewModel, ProjectState>();
        CreateMap<TenantViewModel, AddTenantCommand>();
        CreateMap<TenantViewModel, EditTenantCommand>();
        CreateMap<TenantState, TenantViewModel>().ForPath(e => e.ForeignKeyProject, o => o.MapFrom(s => s.Project!.Name));
        CreateMap<TenantViewModel, TenantState>();
        CreateMap<UserEntityViewModel, AddUserEntityCommand>();
        CreateMap<UserEntityViewModel, EditUserEntityCommand>();
        CreateMap<UserEntityState, UserEntityViewModel>().ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.DatabaseConnectionSetup!.Name + " - " + s.Company!.Code + " - " + s.Company!.Name));
        CreateMap<UserEntityViewModel, UserEntityState>();
        CreateMap<BatchViewModel, AddBatchCommand>();
        CreateMap<BatchViewModel, EditBatchCommand>();
        CreateMap<BatchState, BatchViewModel>().ReverseMap();
        CreateMap<PaymentTransactionViewModel, AddPaymentTransactionCommand>();
        CreateMap<PaymentTransactionViewModel, EditPaymentTransactionCommand>();
        CreateMap<PaymentTransactionState, PaymentTransactionViewModel>().ForPath(e => e.ForeignKeyBatch, o => o.MapFrom(s => s.Batch!.Id)).ForPath(e => e.ForeignKeyEnrolledPayee, o => o.MapFrom(s => s.EnrolledPayee!.Id));
        CreateMap<PaymentTransactionViewModel, PaymentTransactionState>();
        CreateMap<CreditorViewModel, AddCreditorCommand>();
        CreateMap<CreditorViewModel, EditCreditorCommand>();
        CreateMap<CreditorState, CreditorViewModel>().ForPath(e => e.ForeignKeyDatabaseConnectionSetup, o => o.MapFrom(s => s.DatabaseConnectionSetup!.Name));
        CreateMap<CreditorViewModel, CreditorState>();
        CreateMap<EnrolledPayeeViewModel, AddEnrolledPayeeCommand>();
        CreateMap<EnrolledPayeeViewModel, EditEnrolledPayeeCommand>();
        CreateMap<EnrolledPayeeState, EnrolledPayeeViewModel>()
            .ForPath(e => e.ForeignKeyCompany, o => o.MapFrom(s => s.Company!.DatabaseConnectionSetup!.Name + " - " + s.Company!.Code + " - " + s.Company!.Name))
            .ForPath(e => e.ForeignKeyCreditor, o => o.MapFrom(s => s.Creditor!.PayeeAccountName + " - " + s.Creditor!.CreditorAccount))
            .ForPath(e => e.DisableFields, o => o.MapFrom(s => true));
        CreateMap<EnrolledPayeeViewModel, EnrolledPayeeState>();
        CreateMap<EnrolledPayeeEmailViewModel, AddEnrolledPayeeEmailCommand>();
        CreateMap<EnrolledPayeeEmailViewModel, EditEnrolledPayeeEmailCommand>();
        CreateMap<EnrolledPayeeEmailState, EnrolledPayeeEmailViewModel>()
            .ForPath(e => e.ForeignKeyEnrolledPayee, o => o.MapFrom(s => s.EnrolledPayee!.Id));
        CreateMap<EnrolledPayeeEmailViewModel, EnrolledPayeeEmailState>();

        CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
        CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
        CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
        CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
        CreateMap<EnrolledPayeeState, PayeeEnrollmentViewModel>()
            .ForPath(e => e.Company, o => o.MapFrom(s => s.Company!.DatabaseConnectionSetup!.Name + " - " + s.Company!.Code + " - " + s.Company!.Name))
            .ForPath(e => e.Creditor, o => o.MapFrom(s => s.Creditor!.PayeeAccountName + " - " + s.Creditor!.CreditorAccount));
        CreateMap<EnrolledPayeeViewModel, DeactivatePayeeCommand>();
        CreateMap<EnrolledPayeeViewModel, ReEnrollPayeeCommand>();
        CreateMap<PaymentTransactionState, NewPaymentTransactionViewModel>()
            .ForPath(e => e.ForeignKeyBatch, o => o.MapFrom(s => s.Batch == null ? "" : s.Batch!.Date.ToString("yyyyMMdd") + "-" + s.Batch!.Batch))
            .ForPath(e => e.ForeignKeyEnrolledPayee, o => o.MapFrom(s => s.EnrolledPayee!.Creditor!.CreditorDisplayDescription));
        CreateMap<BankViewModel, BankState>().ReverseMap();
    }
}
