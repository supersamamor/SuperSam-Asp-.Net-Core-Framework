using AutoMapper;
using CTI.Common.Core.Mapping;
using CTI.FAS.Application.Features.FAS.Approval.Commands;
using CTI.FAS.Core.FAS;
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



namespace CTI.FAS.Application.Features.FAS;

public class FASProfile : Profile
{
    public FASProfile()
    {
        CreateMap<AddDatabaseConnectionSetupCommand, DatabaseConnectionSetupState>();
		CreateMap <EditDatabaseConnectionSetupCommand, DatabaseConnectionSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddCompanyCommand, CompanyState>();
		CreateMap <EditCompanyCommand, CompanyState>().IgnoreBaseEntityProperties();
		CreateMap<AddProjectCommand, ProjectState>();
		CreateMap <EditProjectCommand, ProjectState>().IgnoreBaseEntityProperties();
		CreateMap<AddTenantCommand, TenantState>();
		CreateMap <EditTenantCommand, TenantState>().IgnoreBaseEntityProperties();
		CreateMap<AddUserEntityCommand, UserEntityState>();
		CreateMap <EditUserEntityCommand, UserEntityState>().IgnoreBaseEntityProperties();
		CreateMap<AddBatchCommand, BatchState>();
		CreateMap <EditBatchCommand, BatchState>().IgnoreBaseEntityProperties();
		CreateMap<AddPaymentTransactionCommand, PaymentTransactionState>();
		CreateMap <EditPaymentTransactionCommand, PaymentTransactionState>().IgnoreBaseEntityProperties();
		CreateMap<AddCreditorCommand, CreditorState>();
		CreateMap <EditCreditorCommand, CreditorState>().IgnoreBaseEntityProperties();
		CreateMap<AddEnrolledPayeeCommand, EnrolledPayeeState>();
		CreateMap <EditEnrolledPayeeCommand, EnrolledPayeeState>().IgnoreBaseEntityProperties();
		CreateMap<AddEnrolledPayeeEmailCommand, EnrolledPayeeEmailState>();
		CreateMap <EditEnrolledPayeeEmailCommand, EnrolledPayeeEmailState>().IgnoreBaseEntityProperties();
		
		CreateMap<EditApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<AddApproverSetupCommand, ApproverSetupState>().IgnoreBaseEntityProperties();
		CreateMap<ApproverAssignmentState, ApproverAssignmentState>().IgnoreBaseEntityProperties();
    }
}
