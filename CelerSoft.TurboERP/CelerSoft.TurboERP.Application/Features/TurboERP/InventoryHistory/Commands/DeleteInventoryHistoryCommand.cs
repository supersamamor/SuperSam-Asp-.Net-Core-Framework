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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Commands;

public record DeleteInventoryHistoryCommand : BaseCommand, IRequest<Validation<Error, InventoryHistoryState>>;

public class DeleteInventoryHistoryCommandHandler : BaseCommandHandler<ApplicationContext, InventoryHistoryState, DeleteInventoryHistoryCommand>, IRequestHandler<DeleteInventoryHistoryCommand, Validation<Error, InventoryHistoryState>>
{
    public DeleteInventoryHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteInventoryHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, InventoryHistoryState>> Handle(DeleteInventoryHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteInventoryHistoryCommandValidator : AbstractValidator<DeleteInventoryHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteInventoryHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<InventoryHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("InventoryHistory with id {PropertyValue} does not exists");
    }
}
