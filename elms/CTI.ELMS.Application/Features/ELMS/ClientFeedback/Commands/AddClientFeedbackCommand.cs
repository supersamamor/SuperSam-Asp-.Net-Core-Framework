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

namespace CTI.ELMS.Application.Features.ELMS.ClientFeedback.Commands;

public record AddClientFeedbackCommand : ClientFeedbackState, IRequest<Validation<Error, ClientFeedbackState>>;

public class AddClientFeedbackCommandHandler : BaseCommandHandler<ApplicationContext, ClientFeedbackState, AddClientFeedbackCommand>, IRequestHandler<AddClientFeedbackCommand, Validation<Error, ClientFeedbackState>>
{
	private readonly IdentityContext _identityContext;
    public AddClientFeedbackCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddClientFeedbackCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ClientFeedbackState>> Handle(AddClientFeedbackCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddClientFeedback(request, cancellationToken));


	public async Task<Validation<Error, ClientFeedbackState>> AddClientFeedback(AddClientFeedbackCommand request, CancellationToken cancellationToken)
	{
		ClientFeedbackState entity = Mapper.Map<ClientFeedbackState>(request);
		UpdateLeadTaskClientFeedBackList(entity);
		UpdateLeadTaskNextStepList(entity);
		UpdateActivityHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ClientFeedbackState>(entity);
	}
	
	private void UpdateLeadTaskClientFeedBackList(ClientFeedbackState entity)
	{
		if (entity.LeadTaskClientFeedBackList?.Count > 0)
		{
			foreach (var leadTaskClientFeedBack in entity.LeadTaskClientFeedBackList!)
			{
				Context.Entry(leadTaskClientFeedBack).State = EntityState.Added;
			}
		}
	}
	private void UpdateLeadTaskNextStepList(ClientFeedbackState entity)
	{
		if (entity.LeadTaskNextStepList?.Count > 0)
		{
			foreach (var leadTaskNextStep in entity.LeadTaskNextStepList!)
			{
				Context.Entry(leadTaskNextStep).State = EntityState.Added;
			}
		}
	}
	private void UpdateActivityHistoryList(ClientFeedbackState entity)
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

public class AddClientFeedbackCommandValidator : AbstractValidator<AddClientFeedbackCommand>
{
    readonly ApplicationContext _context;

    public AddClientFeedbackCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ClientFeedbackState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ClientFeedback with id {PropertyValue} already exists");
        RuleFor(x => x.ClientFeedbackName).MustAsync(async (clientFeedbackName, cancellation) => await _context.NotExists<ClientFeedbackState>(x => x.ClientFeedbackName == clientFeedbackName, cancellationToken: cancellation)).WithMessage("ClientFeedback with clientFeedbackName {PropertyValue} already exists");
	
    }
}
