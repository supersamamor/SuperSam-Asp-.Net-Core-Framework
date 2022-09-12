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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Commands;

public record AddProjectHistoryCommand : ProjectHistoryState, IRequest<Validation<Error, ProjectHistoryState>>;

public class AddProjectHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectHistoryState, AddProjectHistoryCommand>, IRequestHandler<AddProjectHistoryCommand, Validation<Error, ProjectHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ProjectHistoryState>> Handle(AddProjectHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddProjectHistory(request, cancellationToken));


	public async Task<Validation<Error, ProjectHistoryState>> AddProjectHistory(AddProjectHistoryCommand request, CancellationToken cancellationToken)
	{
		ProjectHistoryState entity = Mapper.Map<ProjectHistoryState>(request);
		UpdateProjectDeliverableHistoryList(entity);
		UpdateProjectMilestoneHistoryList(entity);
		UpdateProjectPackageHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectHistoryState>(entity);
	}
	
	private void UpdateProjectDeliverableHistoryList(ProjectHistoryState entity)
	{
		if (entity.ProjectDeliverableHistoryList?.Count > 0)
		{
			foreach (var projectDeliverableHistory in entity.ProjectDeliverableHistoryList!)
			{
				Context.Entry(projectDeliverableHistory).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectMilestoneHistoryList(ProjectHistoryState entity)
	{
		if (entity.ProjectMilestoneHistoryList?.Count > 0)
		{
			foreach (var projectMilestoneHistory in entity.ProjectMilestoneHistoryList!)
			{
				Context.Entry(projectMilestoneHistory).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectPackageHistoryList(ProjectHistoryState entity)
	{
		if (entity.ProjectPackageHistoryList?.Count > 0)
		{
			foreach (var projectPackageHistory in entity.ProjectPackageHistoryList!)
			{
				Context.Entry(projectPackageHistory).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddProjectHistoryCommandValidator : AbstractValidator<AddProjectHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddProjectHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectHistory with id {PropertyValue} already exists");
        
    }
}
