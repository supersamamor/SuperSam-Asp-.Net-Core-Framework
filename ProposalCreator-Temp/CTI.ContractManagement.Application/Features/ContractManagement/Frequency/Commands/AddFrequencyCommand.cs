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

namespace CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Commands;

public record AddFrequencyCommand : FrequencyState, IRequest<Validation<Error, FrequencyState>>;

public class AddFrequencyCommandHandler : BaseCommandHandler<ApplicationContext, FrequencyState, AddFrequencyCommand>, IRequestHandler<AddFrequencyCommand, Validation<Error, FrequencyState>>
{
	private readonly IdentityContext _identityContext;
    public AddFrequencyCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddFrequencyCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, FrequencyState>> Handle(AddFrequencyCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddFrequency(request, cancellationToken));


	public async Task<Validation<Error, FrequencyState>> AddFrequency(AddFrequencyCommand request, CancellationToken cancellationToken)
	{
		FrequencyState entity = Mapper.Map<FrequencyState>(request);
		UpdateProjectMilestoneList(entity);
		UpdateProjectMilestoneHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, FrequencyState>(entity);
	}
	
	private void UpdateProjectMilestoneList(FrequencyState entity)
	{
		if (entity.ProjectMilestoneList?.Count > 0)
		{
			foreach (var projectMilestone in entity.ProjectMilestoneList!)
			{
				Context.Entry(projectMilestone).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectMilestoneHistoryList(FrequencyState entity)
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

public class AddFrequencyCommandValidator : AbstractValidator<AddFrequencyCommand>
{
    readonly ApplicationContext _context;

    public AddFrequencyCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<FrequencyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Frequency with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<FrequencyState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Frequency with name {PropertyValue} already exists");
	
    }
}
