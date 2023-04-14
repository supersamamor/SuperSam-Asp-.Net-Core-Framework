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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Commands;

public record DeleteInventoryCommand : BaseCommand, IRequest<Validation<Error, InventoryState>>;

public class DeleteInventoryCommandHandler : BaseCommandHandler<ApplicationContext, InventoryState, DeleteInventoryCommand>, IRequestHandler<DeleteInventoryCommand, Validation<Error, InventoryState>>
{
    public DeleteInventoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteInventoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, InventoryState>> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteInventoryCommandValidator : AbstractValidator<DeleteInventoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteInventoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<InventoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Inventory with id {PropertyValue} does not exists");
    }
}
