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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Commands;

public record DeletePurchaseRequisitionItemCommand : BaseCommand, IRequest<Validation<Error, PurchaseRequisitionItemState>>;

public class DeletePurchaseRequisitionItemCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseRequisitionItemState, DeletePurchaseRequisitionItemCommand>, IRequestHandler<DeletePurchaseRequisitionItemCommand, Validation<Error, PurchaseRequisitionItemState>>
{
    public DeletePurchaseRequisitionItemCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePurchaseRequisitionItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PurchaseRequisitionItemState>> Handle(DeletePurchaseRequisitionItemCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePurchaseRequisitionItemCommandValidator : AbstractValidator<DeletePurchaseRequisitionItemCommand>
{
    readonly ApplicationContext _context;

    public DeletePurchaseRequisitionItemCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseRequisitionItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseRequisitionItem with id {PropertyValue} does not exists");
    }
}
