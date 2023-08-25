using AutoMapper;
using CTI.DSF.API.Controllers.v1;
using CTI.DSF.Application.Features.DSF.Company.Commands;
using CTI.DSF.Application.Features.DSF.Department.Commands;
using CTI.DSF.Application.Features.DSF.Section.Commands;
using CTI.DSF.Application.Features.DSF.Team.Commands;
using CTI.DSF.Application.Features.DSF.Holiday.Commands;
using CTI.DSF.Application.Features.DSF.Tags.Commands;
using CTI.DSF.Application.Features.DSF.TaskList.Commands;
using CTI.DSF.Application.Features.DSF.TaskApprover.Commands;
using CTI.DSF.Application.Features.DSF.TaskTag.Commands;
using CTI.DSF.Application.Features.DSF.Assignment.Commands;
using CTI.DSF.Application.Features.DSF.Delivery.Commands;


namespace CTI.DSF.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CompanyViewModel, AddCompanyCommand>();
		CreateMap <CompanyViewModel, EditCompanyCommand>();
		CreateMap<DepartmentViewModel, AddDepartmentCommand>();
		CreateMap <DepartmentViewModel, EditDepartmentCommand>();
		CreateMap<SectionViewModel, AddSectionCommand>();
		CreateMap <SectionViewModel, EditSectionCommand>();
		CreateMap<TeamViewModel, AddTeamCommand>();
		CreateMap <TeamViewModel, EditTeamCommand>();
		CreateMap<HolidayViewModel, AddHolidayCommand>();
		CreateMap <HolidayViewModel, EditHolidayCommand>();
		CreateMap<TagsViewModel, AddTagsCommand>();
		CreateMap <TagsViewModel, EditTagsCommand>();
		CreateMap<TaskListViewModel, AddTaskListCommand>();
		CreateMap <TaskListViewModel, EditTaskListCommand>();
		CreateMap<TaskApproverViewModel, AddTaskApproverCommand>();
		CreateMap <TaskApproverViewModel, EditTaskApproverCommand>();
		CreateMap<TaskTagViewModel, AddTaskTagCommand>();
		CreateMap <TaskTagViewModel, EditTaskTagCommand>();
		CreateMap<AssignmentViewModel, AddAssignmentCommand>();
		CreateMap <AssignmentViewModel, EditAssignmentCommand>();
		CreateMap<DeliveryViewModel, AddDeliveryCommand>();
		CreateMap <DeliveryViewModel, EditDeliveryCommand>();
		
    }
}
