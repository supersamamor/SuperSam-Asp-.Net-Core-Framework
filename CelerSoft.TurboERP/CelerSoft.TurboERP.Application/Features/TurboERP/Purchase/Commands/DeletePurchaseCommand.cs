using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Commands;

public record DeletePurchaseCommand : BaseCommand, IRequest<Validation<Error, PurchaseState>>;

public class DeletePurchaseCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseState, DeletePurchaseCommand>, IRequestHandler<DeletePurchaseCommand, Validation<Error, PurchaseState>>
{
    public DeletePurchaseCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePurchaseCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PurchaseState>> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePurchaseCommandValidator : AbstractValidator<DeletePurchaseCommand>
{
    readonly ApplicationContext _context;

    public DeletePurchaseCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Purchase with id {PropertyValue} does not exists");
    }
}
