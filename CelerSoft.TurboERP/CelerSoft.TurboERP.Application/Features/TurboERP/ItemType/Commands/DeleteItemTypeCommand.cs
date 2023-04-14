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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Commands;

public record DeleteItemTypeCommand : BaseCommand, IRequest<Validation<Error, ItemTypeState>>;

public class DeleteItemTypeCommandHandler : BaseCommandHandler<ApplicationContext, ItemTypeState, DeleteItemTypeCommand>, IRequestHandler<DeleteItemTypeCommand, Validation<Error, ItemTypeState>>
{
    public DeleteItemTypeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteItemTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ItemTypeState>> Handle(DeleteItemTypeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteItemTypeCommandValidator : AbstractValidator<DeleteItemTypeCommand>
{
    readonly ApplicationContext _context;

    public DeleteItemTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ItemTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ItemType with id {PropertyValue} does not exists");
    }
}
