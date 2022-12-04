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

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;

public record DeletePaymentTransactionCommand : BaseCommand, IRequest<Validation<Error, PaymentTransactionState>>;

public class DeletePaymentTransactionCommandHandler : BaseCommandHandler<ApplicationContext, PaymentTransactionState, DeletePaymentTransactionCommand>, IRequestHandler<DeletePaymentTransactionCommand, Validation<Error, PaymentTransactionState>>
{
    public DeletePaymentTransactionCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePaymentTransactionCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PaymentTransactionState>> Handle(DeletePaymentTransactionCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePaymentTransactionCommandValidator : AbstractValidator<DeletePaymentTransactionCommand>
{
    readonly ApplicationContext _context;

    public DeletePaymentTransactionCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PaymentTransactionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PaymentTransaction with id {PropertyValue} does not exists");
    }
}
