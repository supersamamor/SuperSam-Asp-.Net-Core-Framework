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

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;

public record AddPaymentTransactionCommand : PaymentTransactionState, IRequest<Validation<Error, PaymentTransactionState>>;

public class AddPaymentTransactionCommandHandler : BaseCommandHandler<ApplicationContext, PaymentTransactionState, AddPaymentTransactionCommand>, IRequestHandler<AddPaymentTransactionCommand, Validation<Error, PaymentTransactionState>>
{
	private readonly IdentityContext _identityContext;
    public AddPaymentTransactionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPaymentTransactionCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PaymentTransactionState>> Handle(AddPaymentTransactionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPaymentTransactionCommandValidator : AbstractValidator<AddPaymentTransactionCommand>
{
    readonly ApplicationContext _context;

    public AddPaymentTransactionCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PaymentTransactionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PaymentTransaction with id {PropertyValue} already exists");
        
    }
}
