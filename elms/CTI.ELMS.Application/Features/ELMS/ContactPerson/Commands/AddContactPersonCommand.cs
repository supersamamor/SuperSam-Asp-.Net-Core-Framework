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

public record AddContactPersonCommand : ContactPersonState, IRequest<Validation<Error, ContactPersonState>>;

public class AddContactPersonCommandHandler : BaseCommandHandler<ApplicationContext, ContactPersonState, AddContactPersonCommand>, IRequestHandler<AddContactPersonCommand, Validation<Error, ContactPersonState>>
{
	private readonly IdentityContext _identityContext;
    public AddContactPersonCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddContactPersonCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ContactPersonState>> Handle(AddContactPersonCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddContactPersonCommandValidator : AbstractValidator<AddContactPersonCommand>
{
    readonly ApplicationContext _context;

    public AddContactPersonCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ContactPersonState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ContactPerson with id {PropertyValue} already exists");
        
    }
}
