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

namespace CTI.ELMS.Application.Features.ELMS.ContactPerson.Commands;

public record EditContactPersonCommand : ContactPersonState, IRequest<Validation<Error, ContactPersonState>>;

public class EditContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, ContactPersonState, EditContactPersonCommand>, IRequestHandler<EditContactPersonCommand, Validation<Error, ContactPersonState>>
{
    public EditContactPersonCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditContactPersonCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ContactPersonState>> Handle(EditContactPersonCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditContactPersonCommandValidator : AbstractValidator<EditContactPersonCommand>
{
    readonly ApplicationContext _context;

    public EditContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ContactPerson with id {PropertyValue} does not exists");
        
    }
}
