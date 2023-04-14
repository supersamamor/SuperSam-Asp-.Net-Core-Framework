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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Commands;

public record EditOrderItemCommand : OrderItemState, IRequest<Validation<Error, OrderItemState>>;

public class EditOrderItemCommandHandler : BaseCommandHandler<ApplicationContext, OrderItemState, EditOrderItemCommand>, IRequestHandler<EditOrderItemCommand, Validation<Error, OrderItemState>>
{
    public EditOrderItemCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditOrderItemCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, OrderItemState>> Handle(EditOrderItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditOrderItemCommandValidator : AbstractValidator<EditOrderItemCommand>
{
    readonly ApplicationContext _context;

    public EditOrderItemCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OrderItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OrderItem with id {PropertyValue} does not exists");
        
    }
}
