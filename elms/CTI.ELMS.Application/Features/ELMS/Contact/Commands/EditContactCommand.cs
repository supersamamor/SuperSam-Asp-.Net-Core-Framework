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

public record EditContactCommand : ContactState, IRequest<Validation<Error, ContactState>>;

public class EditContactCommandHandler : BaseCommandHandler<ApplicationContext, ContactState, EditContactCommand>, IRequestHandler<EditContactCommand, Validation<Error, ContactState>>
{
    public EditContactCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditContactCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ContactState>> Handle(EditContactCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditContactCommandValidator : AbstractValidator<EditContactCommand>
{
    readonly ApplicationContext _context;

    public EditContactCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ContactState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Contact with id {PropertyValue} does not exists");
        
    }
}
