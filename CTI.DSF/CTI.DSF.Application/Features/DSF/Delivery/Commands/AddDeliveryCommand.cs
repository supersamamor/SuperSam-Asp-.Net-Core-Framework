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

public record AddDeliveryCommand : DeliveryState, IRequest<Validation<Error, DeliveryState>>;

public class AddDeliveryCommandHandler : BaseCommandHandler<ApplicationContext, DeliveryState, AddDeliveryCommand>, IRequestHandler<AddDeliveryCommand, Validation<Error, DeliveryState>>
{
	private readonly IdentityContext _identityContext;
    public AddDeliveryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddDeliveryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, DeliveryState>> Handle(AddDeliveryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddDelivery(request, cancellationToken));


	public async Task<Validation<Error, DeliveryState>> AddDelivery(AddDeliveryCommand request, CancellationToken cancellationToken)
	{
		DeliveryState entity = Mapper.Map<DeliveryState>(request);
		_ = await Context.AddAsync(entity, cancellationToken);
		await Helpers.ApprovalHelper.AddApprovers(Context, _identityContext, ApprovalModule.Delivery, entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DeliveryState>(entity);
	}
	
	
}

public class AddDeliveryCommandValidator : AbstractValidator<AddDeliveryCommand>
{
    readonly ApplicationContext _context;

    public AddDeliveryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<DeliveryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Delivery with id {PropertyValue} already exists");
        
    }
}
