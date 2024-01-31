using AutoMapper;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Commands;
using CompanyPL.ProjectPL.Application.DTOs;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Approval.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;


namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Mapping;

public class ProjectPLProfile : Profile
{
    public ProjectPLProfile()
    {
		CreateMap<ReportViewModel, AddReportCommand>()
            .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => new ReportRoleAssignmentState { RoleName = x })));
        CreateMap<ReportViewModel, EditReportCommand>()
           .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => new ReportRoleAssignmentState { RoleName = x })));
        CreateMap<ReportState, ReportViewModel>()
            .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => x.RoleName)));
        CreateMap<ReportViewModel, ReportState>();      
        CreateMap<ReportQueryFilterState, ReportQueryFilterViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportQueryFilterViewModel, ReportQueryFilterState>();
        CreateMap<ReportResultModel, ReportResultViewModel>().ReverseMap();
        CreateMap<ReportQueryFilterModel, ReportQueryFilterViewModel>().ReverseMap();
		
        CreateMap<EmployeeViewModel, AddEmployeeCommand>();
		CreateMap<EmployeeViewModel, EditEmployeeCommand>();
		CreateMap<EmployeeState, EmployeeViewModel>().ReverseMap();
		CreateMap<ContactInformationViewModel, AddContactInformationCommand>();
		CreateMap<ContactInformationViewModel, EditContactInformationCommand>();
		CreateMap<ContactInformationState, ContactInformationViewModel>().ForPath(e => e.ReferenceFieldEmployeeId, o => o.MapFrom(s => s.Employee!.EmployeeCode));
		CreateMap<ContactInformationViewModel, ContactInformationState>();
		CreateMap<HealthDeclarationViewModel, AddHealthDeclarationCommand>();
		CreateMap<HealthDeclarationViewModel, EditHealthDeclarationCommand>();
		CreateMap<HealthDeclarationState, HealthDeclarationViewModel>().ForPath(e => e.ReferenceFieldEmployeeId, o => o.MapFrom(s => s.Employee!.EmployeeCode));
		CreateMap<HealthDeclarationViewModel, HealthDeclarationState>();
		
		CreateMap<ApproverAssignmentState, ApproverAssignmentViewModel>().ReverseMap();
		CreateMap<ApproverSetupViewModel, EditApproverSetupCommand>();
		CreateMap<ApproverSetupViewModel, AddApproverSetupCommand>();
		CreateMap<ApproverSetupState, ApproverSetupViewModel>().ReverseMap();
    }
}
