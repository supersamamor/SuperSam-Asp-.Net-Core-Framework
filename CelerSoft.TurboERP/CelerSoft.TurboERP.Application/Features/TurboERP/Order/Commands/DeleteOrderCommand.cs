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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Order.Commands;

public record DeleteOrderCommand : BaseCommand, IRequest<Validation<Error, OrderState>>;

public class DeleteOrderCommandHandler : BaseCommandHandler<ApplicationContext, OrderState, DeleteOrderCommand>, IRequestHandler<DeleteOrderCommand, Validation<Error, OrderState>>
{
    public DeleteOrderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteOrderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OrderState>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    readonly ApplicationContext _context;

    public DeleteOrderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OrderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Order with id {PropertyValue} does not exists");
    }
}
