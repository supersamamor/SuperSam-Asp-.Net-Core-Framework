using AutoMapper;
using CTI.DSF.API.Controllers.v1;
using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Application.Features.DSF.Assignment.Commands;
using CTI.DSF.Application.Features.DSF.Delivery.Commands;
using CTI.DSF.Application.Features.DSF.Company.Commands;
using CTI.DSF.Application.Features.DSF.Department.Commands;
using CTI.DSF.Application.Features.DSF.Section.Commands;
using CTI.DSF.Application.Features.DSF.Team.Commands;
using CTI.DSF.Application.Features.DSF.Holiday.Commands;

namespace CTI.DSF.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskListViewModel, AddTaskListCommand>();
        CreateMap<TaskListViewModel, EditTaskListCommand>();
        CreateMap<AssignmentViewModel, AddAssignmentCommand>();
        CreateMap<AssignmentViewModel, EditAssignmentCommand>();
        CreateMap<DeliveryViewModel, AddDeliveryCommand>();
        CreateMap<DeliveryViewModel, EditDeliveryCommand>();
        CreateMap<CompanyViewModel, AddCompanyCommand>();
        CreateMap<CompanyViewModel, EditCompanyCommand>();
        CreateMap<DepartmentViewModel, AddDepartmentCommand>();
        CreateMap<DepartmentViewModel, EditDepartmentCommand>();
        CreateMap<SectionViewModel, AddSectionCommand>();
        CreateMap<SectionViewModel, EditSectionCommand>();
        CreateMap<TeamViewModel, AddTeamCommand>();
        CreateMap<TeamViewModel, EditTeamCommand>();
        CreateMap<HolidayViewModel, AddHolidayCommand>();
        CreateMap<HolidayViewModel, EditHolidayCommand>();
    }
}
