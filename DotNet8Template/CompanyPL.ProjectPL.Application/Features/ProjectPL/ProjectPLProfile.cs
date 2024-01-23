using AutoMapper;
using CompanyPL.Common.Core.Mapping;

using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;



namespace CompanyPL.ProjectPL.Application.Features.ProjectPL;

public class ProjectPLProfile : Profile
{
    public ProjectPLProfile()
    {
		CreateMap<AddReportCommand, ReportState>();
        CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		
        CreateMap<AddEmployeeCommand, EmployeeState>();
		CreateMap <EditEmployeeCommand, EmployeeState>().IgnoreBaseEntityProperties();
		CreateMap<AddContactInformationCommand, ContactInformationState>();
		CreateMap <EditContactInformationCommand, ContactInformationState>().IgnoreBaseEntityProperties();
		CreateMap<AddHealthDeclarationCommand, HealthDeclarationState>();
		CreateMap <EditHealthDeclarationCommand, HealthDeclarationState>().IgnoreBaseEntityProperties();
		
		
    }
}
