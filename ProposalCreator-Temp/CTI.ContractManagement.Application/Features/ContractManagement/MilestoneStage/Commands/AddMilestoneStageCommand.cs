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

namespace CTI.ContractManagement.Application.Features.ContractManagement.MilestoneStage.Commands;

public record AddMilestoneStageCommand : MilestoneStageState, IRequest<Validation<Error, MilestoneStageState>>;

public class AddMilestoneStageCommandHandler : BaseCommandHandler<ApplicationContext, MilestoneStageState, AddMilestoneStageCommand>, IRequestHandler<AddMilestoneStageCommand, Validation<Error, MilestoneStageState>>
{
	private readonly IdentityContext _identityContext;
    public AddMilestoneStageCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddMilestoneStageCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, MilestoneStageState>> Handle(AddMilestoneStageCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddMilestoneStage(request, cancellationToken));


	public async Task<Validation<Error, MilestoneStageState>> AddMilestoneStage(AddMilestoneStageCommand request, CancellationToken cancellationToken)
	{
		MilestoneStageState entity = Mapper.Map<MilestoneStageState>(request);
		UpdateProjectMilestoneList(entity);
		UpdateProjectMilestoneHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, MilestoneStageState>(entity);
	}
	
	private void UpdateProjectMilestoneList(MilestoneStageState entity)
	{
		if (entity.ProjectMilestoneList?.Count > 0)
		{
			foreach (var projectMilestone in entity.ProjectMilestoneList!)
			{
				Context.Entry(projectMilestone).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectMilestoneHistoryList(MilestoneStageState entity)
	{
		if (entity.ProjectMilestoneHistoryList?.Count > 0)
		{
			foreach (var projectMilestoneHistory in entity.ProjectMilestoneHistoryList!)
			{
				Context.Entry(projectMilestoneHistory).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddMilestoneStageCommandValidator : AbstractValidator<AddMilestoneStageCommand>
{
    readonly ApplicationContext _context;

    public AddMilestoneStageCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<MilestoneStageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MilestoneStage with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<MilestoneStageState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("MilestoneStage with name {PropertyValue} already exists");
	
    }
}
