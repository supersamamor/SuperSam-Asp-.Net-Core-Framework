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

namespace CTI.ELMS.Application.Features.ELMS.LeadTask.Commands;

public record AddLeadTaskCommand : LeadTaskState, IRequest<Validation<Error, LeadTaskState>>;

public class AddLeadTaskCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskState, AddLeadTaskCommand>, IRequestHandler<AddLeadTaskCommand, Validation<Error, LeadTaskState>>
{
	private readonly IdentityContext _identityContext;
    public AddLeadTaskCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLeadTaskCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, LeadTaskState>> Handle(AddLeadTaskCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddLeadTask(request, cancellationToken));


	public async Task<Validation<Error, LeadTaskState>> AddLeadTask(AddLeadTaskCommand request, CancellationToken cancellationToken)
	{
		LeadTaskState entity = Mapper.Map<LeadTaskState>(request);
		UpdateActivityHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, LeadTaskState>(entity);
	}
	
	private void UpdateActivityHistoryList(LeadTaskState entity)
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

public class AddLeadTaskCommandValidator : AbstractValidator<AddLeadTaskCommand>
{
    readonly ApplicationContext _context;

    public AddLeadTaskCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LeadTaskState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTask with id {PropertyValue} already exists");
        RuleFor(x => x.LeadTaskName).MustAsync(async (leadTaskName, cancellation) => await _context.NotExists<LeadTaskState>(x => x.LeadTaskName == leadTaskName, cancellationToken: cancellation)).WithMessage("LeadTask with leadTaskName {PropertyValue} already exists");
	
    }
}
