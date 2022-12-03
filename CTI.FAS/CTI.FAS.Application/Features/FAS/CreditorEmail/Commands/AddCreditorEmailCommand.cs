using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.CreditorEmail.Commands;

public record AddCreditorEmailCommand : CreditorEmailState, IRequest<Validation<Error, CreditorEmailState>>;

public class AddCreditorEmailCommandHandler : BaseCommandHandler<ApplicationContext, CreditorEmailState, AddCreditorEmailCommand>, IRequestHandler<AddCreditorEmailCommand, Validation<Error, CreditorEmailState>>
{
	private readonly IdentityContext _identityContext;
    public AddCreditorEmailCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCreditorEmailCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, CreditorEmailState>> Handle(AddCreditorEmailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddCreditorEmailCommandValidator : AbstractValidator<AddCreditorEmailCommand>
{
    readonly ApplicationContext _context;

    public AddCreditorEmailCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CreditorEmailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CreditorEmail with id {PropertyValue} already exists");
        
    }
}
