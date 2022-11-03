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

public record EditLeadTaskNextStepCommand : LeadTaskNextStepState, IRequest<Validation<Error, LeadTaskNextStepState>>;

public class EditLeadTaskNextStepCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskNextStepState, EditLeadTaskNextStepCommand>, IRequestHandler<EditLeadTaskNextStepCommand, Validation<Error, LeadTaskNextStepState>>
{
    public EditLeadTaskNextStepCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLeadTaskNextStepCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, LeadTaskNextStepState>> Handle(EditLeadTaskNextStepCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditLeadTaskNextStepCommandValidator : AbstractValidator<EditLeadTaskNextStepCommand>
{
    readonly ApplicationContext _context;

    public EditLeadTaskNextStepCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTaskNextStepState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTaskNextStep with id {PropertyValue} does not exists");
        
    }
}
