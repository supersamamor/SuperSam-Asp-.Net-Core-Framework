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

namespace CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Commands;

public record AddIFCATransactionTypeCommand : IFCATransactionTypeState, IRequest<Validation<Error, IFCATransactionTypeState>>;

public class AddIFCATransactionTypeCommandHandler : BaseCommandHandler<ApplicationContext, IFCATransactionTypeState, AddIFCATransactionTypeCommand>, IRequestHandler<AddIFCATransactionTypeCommand, Validation<Error, IFCATransactionTypeState>>
{
	private readonly IdentityContext _identityContext;
    public AddIFCATransactionTypeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddIFCATransactionTypeCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, IFCATransactionTypeState>> Handle(AddIFCATransactionTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddIFCATransactionTypeCommandValidator : AbstractValidator<AddIFCATransactionTypeCommand>
{
    readonly ApplicationContext _context;

    public AddIFCATransactionTypeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<IFCATransactionTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCATransactionType with id {PropertyValue} already exists");
        
    }
}
