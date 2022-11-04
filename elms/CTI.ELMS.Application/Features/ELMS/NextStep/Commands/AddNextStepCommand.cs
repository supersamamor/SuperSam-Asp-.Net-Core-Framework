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

namespace CTI.ELMS.Application.Features.ELMS.NextStep.Commands;

public record AddNextStepCommand : NextStepState, IRequest<Validation<Error, NextStepState>>;

public class AddNextStepCommandHandler : BaseCommandHandler<ApplicationContext, NextStepState, AddNextStepCommand>, IRequestHandler<AddNextStepCommand, Validation<Error, NextStepState>>
{
	private readonly IdentityContext _identityContext;
    public AddNextStepCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddNextStepCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, NextStepState>> Handle(AddNextStepCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddNextStep(request, cancellationToken));


	public async Task<Validation<Error, NextStepState>> AddNextStep(AddNextStepCommand request, CancellationToken cancellationToken)
	{
		NextStepState entity = Mapper.Map<NextStepState>(request);
		UpdateLeadTaskNextStepList(entity);
		UpdateActivityHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, NextStepState>(entity);
	}
	
	private void UpdateLeadTaskNextStepList(NextStepState entity)
	{
		if (entity.LeadTaskNextStepList?.Count > 0)
		{
			foreach (var leadTaskNextStep in entity.LeadTaskNextStepList!)
			{
				Context.Entry(leadTaskNextStep).State = EntityState.Added;
			}
		}
	}
	private void UpdateActivityHistoryList(NextStepState entity)
	{
		if (entity.ActivityHistoryList?.Count > 0)
		{
			foreach (var activityHistory in entity.ActivityHistoryList!)
			{
				Context.Entry(activityHistory).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddNextStepCommandValidator : AbstractValidator<AddNextStepCommand>
{
    readonly ApplicationContext _context;

    public AddNextStepCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<NextStepState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("NextStep with id {PropertyValue} already exists");
        RuleFor(x => x.NextStepTaskName).MustAsync(async (nextStepTaskName, cancellation) => await _context.NotExists<NextStepState>(x => x.NextStepTaskName == nextStepTaskName, cancellationToken: cancellation)).WithMessage("NextStep with nextStepTaskName {PropertyValue} already exists");
	
    }
}
