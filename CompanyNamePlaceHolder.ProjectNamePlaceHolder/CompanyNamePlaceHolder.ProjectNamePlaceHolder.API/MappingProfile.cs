using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.TaskList.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Assignment.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Commands;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskListViewModel, AddTaskListCommand>();
		CreateMap <TaskListViewModel, EditTaskListCommand>();
		CreateMap<AssignmentViewModel, AddAssignmentCommand>();
		CreateMap <AssignmentViewModel, EditAssignmentCommand>();
		CreateMap<DeliveryViewModel, AddDeliveryCommand>();
		CreateMap <DeliveryViewModel, EditDeliveryCommand>();
		
    }
}
