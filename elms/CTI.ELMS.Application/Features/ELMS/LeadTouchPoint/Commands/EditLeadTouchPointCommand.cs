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

namespace CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Commands;

public record EditLeadTouchPointCommand : LeadTouchPointState, IRequest<Validation<Error, LeadTouchPointState>>;

public class EditLeadTouchPointCommandHandler : BaseCommandHandler<ApplicationContext, LeadTouchPointState, EditLeadTouchPointCommand>, IRequestHandler<EditLeadTouchPointCommand, Validation<Error, LeadTouchPointState>>
{
    public EditLeadTouchPointCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLeadTouchPointCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, LeadTouchPointState>> Handle(EditLeadTouchPointCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditLeadTouchPointCommandValidator : AbstractValidator<EditLeadTouchPointCommand>
{
    readonly ApplicationContext _context;

    public EditLeadTouchPointCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTouchPointState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTouchPoint with id {PropertyValue} does not exists");
        RuleFor(x => x.LeadTouchPointName).MustAsync(async (request, leadTouchPointName, cancellation) => await _context.NotExists<LeadTouchPointState>(x => x.LeadTouchPointName == leadTouchPointName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("LeadTouchPoint with leadTouchPointName {PropertyValue} already exists");
	
    }
}
