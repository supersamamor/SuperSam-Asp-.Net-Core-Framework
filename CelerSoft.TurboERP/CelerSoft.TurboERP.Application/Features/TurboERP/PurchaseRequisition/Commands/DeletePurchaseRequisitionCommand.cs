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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Commands;

public record DeletePurchaseRequisitionCommand : BaseCommand, IRequest<Validation<Error, PurchaseRequisitionState>>;

public class DeletePurchaseRequisitionCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseRequisitionState, DeletePurchaseRequisitionCommand>, IRequestHandler<DeletePurchaseRequisitionCommand, Validation<Error, PurchaseRequisitionState>>
{
    public DeletePurchaseRequisitionCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePurchaseRequisitionCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PurchaseRequisitionState>> Handle(DeletePurchaseRequisitionCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePurchaseRequisitionCommandValidator : AbstractValidator<DeletePurchaseRequisitionCommand>
{
    readonly ApplicationContext _context;

    public DeletePurchaseRequisitionCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseRequisitionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseRequisition with id {PropertyValue} does not exists");
    }
}
