using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.TaskList.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Assignment.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Delivery.Commands;


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
