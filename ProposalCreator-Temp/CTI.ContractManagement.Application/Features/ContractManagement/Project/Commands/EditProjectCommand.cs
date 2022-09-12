using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Project.Commands;

public record EditProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class EditProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, EditProjectCommand>, IRequestHandler<EditProjectCommand, Validation<Error, ProjectState>>
{
    public EditProjectCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(EditProjectCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditProject(request, cancellationToken));


	public async Task<Validation<Error, ProjectState>> EditProject(EditProjectCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Project.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProjectDeliverableList(entity, request, cancellationToken);
		await UpdateProjectMilestoneList(entity, request, cancellationToken);
		await UpdateProjectPackageList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectState>(entity);
	}
	
	private async Task UpdateProjectDeliverableList(ProjectState entity, EditProjectCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectDeliverableState> projectDeliverableListForDeletion = new List<ProjectDeliverableState>();
		var queryProjectDeliverableForDeletion = Context.ProjectDeliverable.Where(l => l.ProjectId == request.Id).AsNoTracking();
		if (entity.ProjectDeliverableList?.Count > 0)
		{
			queryProjectDeliverableForDeletion = queryProjectDeliverableForDeletion.Where(l => !(entity.ProjectDeliverableList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectDeliverableListForDeletion = await queryProjectDeliverableForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectDeliverable in projectDeliverableListForDeletion!)
		{
			Context.Entry(projectDeliverable).State = EntityState.Deleted;
		}
		if (entity.ProjectDeliverableList?.Count > 0)
		{
			foreach (var projectDeliverable in entity.ProjectDeliverableList.Where(l => !projectDeliverableListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectDeliverableState>(x => x.Id == projectDeliverable.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectDeliverable).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectDeliverable).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateProjectMilestoneList(ProjectState entity, EditProjectCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectMilestoneState> projectMilestoneListForDeletion = new List<ProjectMilestoneState>();
		var queryProjectMilestoneForDeletion = Context.ProjectMilestone.Where(l => l.ProjectId == request.Id).AsNoTracking();
		if (entity.ProjectMilestoneList?.Count > 0)
		{
			queryProjectMilestoneForDeletion = queryProjectMilestoneForDeletion.Where(l => !(entity.ProjectMilestoneList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectMilestoneListForDeletion = await queryProjectMilestoneForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectMilestone in projectMilestoneListForDeletion!)
		{
			Context.Entry(projectMilestone).State = EntityState.Deleted;
		}
		if (entity.ProjectMilestoneList?.Count > 0)
		{
			foreach (var projectMilestone in entity.ProjectMilestoneList.Where(l => !projectMilestoneListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectMilestoneState>(x => x.Id == projectMilestone.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectMilestone).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectMilestone).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateProjectPackageList(ProjectState entity, EditProjectCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectPackageState> projectPackageListForDeletion = new List<ProjectPackageState>();
		var queryProjectPackageForDeletion = Context.ProjectPackage.Where(l => l.ProjectId == request.Id).AsNoTracking();
		if (entity.ProjectPackageList?.Count > 0)
		{
			queryProjectPackageForDeletion = queryProjectPackageForDeletion.Where(l => !(entity.ProjectPackageList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectPackageListForDeletion = await queryProjectPackageForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectPackage in projectPackageListForDeletion!)
		{
			Context.Entry(projectPackage).State = EntityState.Deleted;
		}
		if (entity.ProjectPackageList?.Count > 0)
		{
			foreach (var projectPackage in entity.ProjectPackageList.Where(l => !projectPackageListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectPackageState>(x => x.Id == projectPackage.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectPackage).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectPackage).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditProjectCommandValidator : AbstractValidator<EditProjectCommand>
{
    readonly ApplicationContext _context;

    public EditProjectCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Project with id {PropertyValue} does not exists");
        RuleFor(x => x.DocumentCode).MustAsync(async (request, documentCode, cancellation) => await _context.NotExists<ProjectState>(x => x.DocumentCode == documentCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Project with documentCode {PropertyValue} already exists");
	
    }
}
