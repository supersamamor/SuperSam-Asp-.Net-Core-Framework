using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.Project.Commands;

public record AddProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class AddProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, AddProjectCommand>, IRequestHandler<AddProjectCommand, Validation<Error, ProjectState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ProjectState>> Handle(AddProjectCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddProject(request, cancellationToken));


	public async Task<Validation<Error, ProjectState>> AddProject(AddProjectCommand request, CancellationToken cancellationToken)
	{
		ProjectState entity = Mapper.Map<ProjectState>(request);
		UpdateUserProjectAssignmentList(entity);
		UpdateOfferingHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectState>(entity);
	}
	
	private void UpdateUserProjectAssignmentList(ProjectState entity)
	{
		if (entity.UserProjectAssignmentList?.Count > 0)
		{
			foreach (var userProjectAssignment in entity.UserProjectAssignmentList!)
			{
				Context.Entry(userProjectAssignment).State = EntityState.Added;
			}
		}
	}
	private void UpdateOfferingHistoryList(ProjectState entity)
	{
		if (entity.OfferingHistoryList?.Count > 0)
		{
			foreach (var offeringHistory in entity.OfferingHistoryList!)
			{
				Context.Entry(offeringHistory).State = EntityState.Added;
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
