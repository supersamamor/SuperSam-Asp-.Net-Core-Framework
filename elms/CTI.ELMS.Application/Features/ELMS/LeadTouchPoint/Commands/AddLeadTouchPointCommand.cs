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

public record AddLeadTouchPointCommand : LeadTouchPointState, IRequest<Validation<Error, LeadTouchPointState>>;

public class AddLeadTouchPointCommandHandler : BaseCommandHandler<ApplicationContext, LeadTouchPointState, AddLeadTouchPointCommand>, IRequestHandler<AddLeadTouchPointCommand, Validation<Error, LeadTouchPointState>>
{
	private readonly IdentityContext _identityContext;
    public AddLeadTouchPointCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLeadTouchPointCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, LeadTouchPointState>> Handle(AddLeadTouchPointCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddLeadTouchPointCommandValidator : AbstractValidator<AddLeadTouchPointCommand>
{
    readonly ApplicationContext _context;

    public AddLeadTouchPointCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LeadTouchPointState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTouchPoint with id {PropertyValue} already exists");
        RuleFor(x => x.LeadTouchPointName).MustAsync(async (leadTouchPointName, cancellation) => await _context.NotExists<LeadTouchPointState>(x => x.LeadTouchPointName == leadTouchPointName, cancellationToken: cancellation)).WithMessage("LeadTouchPoint with leadTouchPointName {PropertyValue} already exists");
	
    }
}
