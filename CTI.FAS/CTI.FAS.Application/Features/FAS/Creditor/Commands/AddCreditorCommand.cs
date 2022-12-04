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

namespace CTI.FAS.Application.Features.FAS.Creditor.Commands;

public record AddCreditorCommand : CreditorState, IRequest<Validation<Error, CreditorState>>;

public class AddCreditorCommandHandler : BaseCommandHandler<ApplicationContext, CreditorState, AddCreditorCommand>, IRequestHandler<AddCreditorCommand, Validation<Error, CreditorState>>
{
	private readonly IdentityContext _identityContext;
    public AddCreditorCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCreditorCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, CreditorState>> Handle(AddCreditorCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddCreditorCommandValidator : AbstractValidator<AddCreditorCommand>
{
    readonly ApplicationContext _context;

    public AddCreditorCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CreditorState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Creditor with id {PropertyValue} already exists");
        
    }
}
