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

namespace CTI.ELMS.Application.Features.ELMS.Contact.Commands;

public record AddContactCommand : ContactState, IRequest<Validation<Error, ContactState>>;

public class AddContactCommandHandler : BaseCommandHandler<ApplicationContext, ContactState, AddContactCommand>, IRequestHandler<AddContactCommand, Validation<Error, ContactState>>
{
	private readonly IdentityContext _identityContext;
    public AddContactCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddContactCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ContactState>> Handle(AddContactCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddContactCommandValidator : AbstractValidator<AddContactCommand>
{
    readonly ApplicationContext _context;

    public AddContactCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ContactState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Contact with id {PropertyValue} already exists");
        
    }
}
