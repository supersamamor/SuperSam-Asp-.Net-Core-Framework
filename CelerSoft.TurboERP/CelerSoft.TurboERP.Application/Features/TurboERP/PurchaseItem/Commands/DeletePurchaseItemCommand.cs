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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Commands;

public record DeletePurchaseItemCommand : BaseCommand, IRequest<Validation<Error, PurchaseItemState>>;

public class DeletePurchaseItemCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseItemState, DeletePurchaseItemCommand>, IRequestHandler<DeletePurchaseItemCommand, Validation<Error, PurchaseItemState>>
{
    public DeletePurchaseItemCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePurchaseItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PurchaseItemState>> Handle(DeletePurchaseItemCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePurchaseItemCommandValidator : AbstractValidator<DeletePurchaseItemCommand>
{
    readonly ApplicationContext _context;

    public DeletePurchaseItemCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseItem with id {PropertyValue} does not exists");
    }
}
