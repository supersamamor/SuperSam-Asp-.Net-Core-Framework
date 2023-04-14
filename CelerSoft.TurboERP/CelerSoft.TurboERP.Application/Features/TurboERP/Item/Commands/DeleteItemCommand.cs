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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Item.Commands;

public record DeleteItemCommand : BaseCommand, IRequest<Validation<Error, ItemState>>;

public class DeleteItemCommandHandler : BaseCommandHandler<ApplicationContext, ItemState, DeleteItemCommand>, IRequestHandler<DeleteItemCommand, Validation<Error, ItemState>>
{
    public DeleteItemCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ItemState>> Handle(DeleteItemCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    readonly ApplicationContext _context;

    public DeleteItemCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Item with id {PropertyValue} does not exists");
    }
}
