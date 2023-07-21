using AutoMapper;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;

using CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;


namespace CompanyPL.EISPL.Web.Areas.EISPL.Mapping;

public class EISPLProfile : Profile
{
    public EISPLProfile()
    {
        CreateMap<PLEmployeeViewModel, AddPLEmployeeCommand>();
		CreateMap<PLEmployeeViewModel, EditPLEmployeeCommand>();
		CreateMap<PLEmployeeState, PLEmployeeViewModel>().ReverseMap();
		CreateMap<PLContactInformationViewModel, AddPLContactInformationCommand>();
		CreateMap<PLContactInformationViewModel, EditPLContactInformationCommand>();
		CreateMap<PLContactInformationState, PLContactInformationViewModel>().ForPath(e => e.ForeignKeyPLEmployee, o => o.MapFrom(s => s.PLEmployee!.PLEmployeeCode));
		CreateMap<PLContactInformationViewModel, PLContactInformationState>();
		CreateMap<PLHealthDeclarationViewModel, AddPLHealthDeclarationCommand>();
		CreateMap<PLHealthDeclarationViewModel, EditPLHealthDeclarationCommand>();
		CreateMap<PLHealthDeclarationState, PLHealthDeclarationViewModel>().ForPath(e => e.ForeignKeyPLEmployee, o => o.MapFrom(s => s.PLEmployee!.PLEmployeeCode));
		CreateMap<PLHealthDeclarationViewModel, PLHealthDeclarationState>();
		CreateMap<TestViewModel, AddTestCommand>();
		CreateMap<TestViewModel, EditTestCommand>();
		CreateMap<TestState, TestViewModel>().ForPath(e => e.ForeignKeyPLEmployee, o => o.MapFrom(s => s.PLEmployee!.PLEmployeeCode));
		CreateMap<TestViewModel, TestState>();
		
		
    }
}
