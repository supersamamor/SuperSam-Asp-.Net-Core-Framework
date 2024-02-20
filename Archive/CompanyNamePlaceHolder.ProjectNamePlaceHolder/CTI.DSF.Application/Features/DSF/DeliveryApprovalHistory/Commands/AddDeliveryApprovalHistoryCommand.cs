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

namespace CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Commands;

public record AddDeliveryApprovalHistoryCommand : DeliveryApprovalHistoryState, IRequest<Validation<Error, DeliveryApprovalHistoryState>>;

public class AddDeliveryApprovalHistoryCommandHandler : BaseCommandHandler<ApplicationContext, DeliveryApprovalHistoryState, AddDeliveryApprovalHistoryCommand>, IRequestHandler<AddDeliveryApprovalHistoryCommand, Validation<Error, DeliveryApprovalHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddDeliveryApprovalHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddDeliveryApprovalHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, DeliveryApprovalHistoryState>> Handle(AddDeliveryApprovalHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddDeliveryApprovalHistory(request, cancellationToken));


	public async Task<Validation<Error, DeliveryApprovalHistoryState>> AddDeliveryApprovalHistory(AddDeliveryApprovalHistoryCommand request, CancellationToken cancellationToken)
	{
		DeliveryApprovalHistoryState entity = Mapper.Map<DeliveryApprovalHistoryState>(request);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DeliveryApprovalHistoryState>(entity);
	}
	
	
}

public class AddDeliveryApprovalHistoryCommandValidator : AbstractValidator<AddDeliveryApprovalHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddDeliveryApprovalHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<DeliveryApprovalHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("DeliveryApprovalHistory with id {PropertyValue} already exists");
        
    }
}
