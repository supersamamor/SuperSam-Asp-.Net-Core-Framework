using AutoMapper;
using CompanyPL.Common.Core.Mapping;

using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;



namespace CompanyPL.EISPL.Application.Features.EISPL;

public class EISPLProfile : Profile
{
    public EISPLProfile()
    {
        CreateMap<AddPLEmployeeCommand, PLEmployeeState>();
		CreateMap <EditPLEmployeeCommand, PLEmployeeState>().IgnoreBaseEntityProperties();
		CreateMap<AddPLContactInformationCommand, PLContactInformationState>();
		CreateMap <EditPLContactInformationCommand, PLContactInformationState>().IgnoreBaseEntityProperties();
		CreateMap<AddPLHealthDeclarationCommand, PLHealthDeclarationState>();
		CreateMap <EditPLHealthDeclarationCommand, PLHealthDeclarationState>().IgnoreBaseEntityProperties();
		CreateMap<AddTestCommand, TestState>();
		CreateMap <EditTestCommand, TestState>().IgnoreBaseEntityProperties();
		
		
    }
}
