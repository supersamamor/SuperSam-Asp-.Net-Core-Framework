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

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Commands;

public record AddLeadTaskNextStepCommand : LeadTaskNextStepState, IRequest<Validation<Error, LeadTaskNextStepState>>;

public class AddLeadTaskNextStepCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskNextStepState, AddLeadTaskNextStepCommand>, IRequestHandler<AddLeadTaskNextStepCommand, Validation<Error, LeadTaskNextStepState>>
{
	private readonly IdentityContext _identityContext;
    public AddLeadTaskNextStepCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLeadTaskNextStepCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, LeadTaskNextStepState>> Handle(AddLeadTaskNextStepCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddLeadTaskNextStepCommandValidator : AbstractValidator<AddLeadTaskNextStepCommand>
{
    readonly ApplicationContext _context;

    public AddLeadTaskNextStepCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LeadTaskNextStepState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTaskNextStep with id {PropertyValue} already exists");
        
    }
}
