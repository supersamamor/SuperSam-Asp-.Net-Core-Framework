using AutoMapper;
using CompanyPL.ProjectPL.API.Controllers.v1;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;


namespace CompanyPL.ProjectPL.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SampleParentViewModel, AddSampleParentCommand>();
		CreateMap <SampleParentViewModel, EditSampleParentCommand>();
		CreateMap<EmployeeViewModel, AddEmployeeCommand>();
		CreateMap <EmployeeViewModel, EditEmployeeCommand>();
		CreateMap<ContactInformationViewModel, AddContactInformationCommand>();
		CreateMap <ContactInformationViewModel, EditContactInformationCommand>();
		CreateMap<HealthDeclarationViewModel, AddHealthDeclarationCommand>();
		CreateMap <HealthDeclarationViewModel, EditHealthDeclarationCommand>();
		
    }
}
