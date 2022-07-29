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

public record AddProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class AddProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, AddProjectCommand>, IRequestHandler<AddProjectCommand, Validation<Error, ProjectState>>
{
    public AddProjectCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(AddProjectCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddProject(request, cancellationToken));


	public async Task<Validation<Error, ProjectState>> AddProject(AddProjectCommand request, CancellationToken cancellationToken)
	{
		ProjectState entity = Mapper.Map<ProjectState>(request);
		UpdateProjectBusinessUnitList(entity);
		UpdateLevelList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectState>(entity);
	}
	
	private void UpdateProjectBusinessUnitList(ProjectState entity)
	{
		if (entity.ProjectBusinessUnitList?.Count > 0)
		{
			foreach (var projectBusinessUnit in entity.ProjectBusinessUnitList!)
			{
				Context.Entry(projectBusinessUnit).State = EntityState.Added;
			}
		}
	}
	private void UpdateLevelList(ProjectState entity)
	{
		if (entity.LevelList?.Count > 0)
		{
			foreach (var level in entity.LevelList!)
			{
				Context.Entry(level).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddProjectCommandValidator : AbstractValidator<AddProjectCommand>
{
    readonly ApplicationContext _context;

    public AddProjectCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Project with id {PropertyValue} already exists");
        
    }
}
