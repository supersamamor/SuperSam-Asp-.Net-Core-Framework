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

public record AddOrderItemCommand : OrderItemState, IRequest<Validation<Error, OrderItemState>>;

public class AddOrderItemCommandHandler : BaseCommandHandler<ApplicationContext, OrderItemState, AddOrderItemCommand>, IRequestHandler<AddOrderItemCommand, Validation<Error, OrderItemState>>
{
	private readonly IdentityContext _identityContext;
    public AddOrderItemCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddOrderItemCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, OrderItemState>> Handle(AddOrderItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    readonly ApplicationContext _context;

    public AddOrderItemCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<OrderItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OrderItem with id {PropertyValue} already exists");
        
    }
}
