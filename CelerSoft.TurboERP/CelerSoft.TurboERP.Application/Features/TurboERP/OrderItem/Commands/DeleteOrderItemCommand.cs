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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Commands;

public record DeleteOrderItemCommand : BaseCommand, IRequest<Validation<Error, OrderItemState>>;

public class DeleteOrderItemCommandHandler : BaseCommandHandler<ApplicationContext, OrderItemState, DeleteOrderItemCommand>, IRequestHandler<DeleteOrderItemCommand, Validation<Error, OrderItemState>>
{
    public DeleteOrderItemCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteOrderItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OrderItemState>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
{
    readonly ApplicationContext _context;

    public DeleteOrderItemCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OrderItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OrderItem with id {PropertyValue} does not exists");
    }
}
