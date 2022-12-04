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

public record EditPaymentTransactionCommand : PaymentTransactionState, IRequest<Validation<Error, PaymentTransactionState>>;

public class EditPaymentTransactionCommandHandler : BaseCommandHandler<ApplicationContext, PaymentTransactionState, EditPaymentTransactionCommand>, IRequestHandler<EditPaymentTransactionCommand, Validation<Error, PaymentTransactionState>>
{
    public EditPaymentTransactionCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPaymentTransactionCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PaymentTransactionState>> Handle(EditPaymentTransactionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPaymentTransactionCommandValidator : AbstractValidator<EditPaymentTransactionCommand>
{
    readonly ApplicationContext _context;

    public EditPaymentTransactionCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PaymentTransactionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PaymentTransaction with id {PropertyValue} does not exists");
        
    }
}
