using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Delivery.Commands;

public record EditDeliveryCommand : DeliveryState, IRequest<Validation<Error, DeliveryState>>;

public class EditDeliveryCommandHandler : BaseCommandHandler<ApplicationContext, DeliveryState, EditDeliveryCommand>, IRequestHandler<EditDeliveryCommand, Validation<Error, DeliveryState>>
{
    public EditDeliveryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDeliveryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, DeliveryState>> Handle(EditDeliveryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditDeliveryCommandValidator : AbstractValidator<EditDeliveryCommand>
{
    readonly ApplicationContext _context;

    public EditDeliveryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DeliveryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Delivery with id {PropertyValue} does not exists");
        
    }
}
