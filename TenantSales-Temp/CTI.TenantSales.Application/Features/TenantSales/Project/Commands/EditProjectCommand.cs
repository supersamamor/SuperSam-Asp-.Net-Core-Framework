using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.Project.Commands;

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
		await UpdateProjectBusinessUnitList(entity, request, cancellationToken);
		await UpdateLevelList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectState>(entity);
	}
	
	private async Task UpdateProjectBusinessUnitList(ProjectState entity, EditProjectCommand request, CancellationToken cancellationToken)
	{
		IList<ProjectBusinessUnitState> projectBusinessUnitListForDeletion = new List<ProjectBusinessUnitState>();
		var queryProjectBusinessUnitForDeletion = Context.ProjectBusinessUnit.Where(l => l.ProjectId == request.Id).AsNoTracking();
		if (entity.ProjectBusinessUnitList?.Count > 0)
		{
			queryProjectBusinessUnitForDeletion = queryProjectBusinessUnitForDeletion.Where(l => !(entity.ProjectBusinessUnitList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		projectBusinessUnitListForDeletion = await queryProjectBusinessUnitForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var projectBusinessUnit in projectBusinessUnitListForDeletion!)
		{
			Context.Entry(projectBusinessUnit).State = EntityState.Deleted;
		}
		if (entity.ProjectBusinessUnitList?.Count > 0)
		{
			foreach (var projectBusinessUnit in entity.ProjectBusinessUnitList.Where(l => !projectBusinessUnitListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProjectBusinessUnitState>(x => x.Id == projectBusinessUnit.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(projectBusinessUnit).State = EntityState.Added;
				}
				else
				{
					Context.Entry(projectBusinessUnit).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateLevelList(ProjectState entity, EditProjectCommand request, CancellationToken cancellationToken)
	{
		IList<LevelState> levelListForDeletion = new List<LevelState>();
		var queryLevelForDeletion = Context.Level.Where(l => l.ProjectId == request.Id).AsNoTracking();
		if (entity.LevelList?.Count > 0)
		{
			queryLevelForDeletion = queryLevelForDeletion.Where(l => !(entity.LevelList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		levelListForDeletion = await queryLevelForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var level in levelListForDeletion!)
		{
			Context.Entry(level).State = EntityState.Deleted;
		}
		if (entity.LevelList?.Count > 0)
		{
			foreach (var level in entity.LevelList.Where(l => !levelListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<LevelState>(x => x.Id == level.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(level).State = EntityState.Added;
				}
				else
				{
					Context.Entry(level).State = EntityState.Modified;
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
        
    }
}
