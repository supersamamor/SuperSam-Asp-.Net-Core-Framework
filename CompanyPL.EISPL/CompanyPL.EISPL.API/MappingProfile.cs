using AutoMapper;
using CompanyPL.EISPL.API.Controllers.v1;
using CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;


namespace CompanyPL.EISPL.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PLEmployeeViewModel, AddPLEmployeeCommand>();
		CreateMap <PLEmployeeViewModel, EditPLEmployeeCommand>();
		CreateMap<PLContactInformationViewModel, AddPLContactInformationCommand>();
		CreateMap <PLContactInformationViewModel, EditPLContactInformationCommand>();
		CreateMap<PLHealthDeclarationViewModel, AddPLHealthDeclarationCommand>();
		CreateMap <PLHealthDeclarationViewModel, EditPLHealthDeclarationCommand>();
		CreateMap<TestViewModel, AddTestCommand>();
		CreateMap <TestViewModel, EditTestCommand>();
		
    }
}
