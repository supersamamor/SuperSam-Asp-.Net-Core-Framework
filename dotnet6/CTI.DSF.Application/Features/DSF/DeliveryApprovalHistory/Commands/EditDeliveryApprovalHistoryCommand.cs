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

public record EditDeliveryApprovalHistoryCommand : DeliveryApprovalHistoryState, IRequest<Validation<Error, DeliveryApprovalHistoryState>>;

public class EditDeliveryApprovalHistoryCommandHandler : BaseCommandHandler<ApplicationContext, DeliveryApprovalHistoryState, EditDeliveryApprovalHistoryCommand>, IRequestHandler<EditDeliveryApprovalHistoryCommand, Validation<Error, DeliveryApprovalHistoryState>>
{
    public EditDeliveryApprovalHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDeliveryApprovalHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, DeliveryApprovalHistoryState>> Handle(EditDeliveryApprovalHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditDeliveryApprovalHistoryCommandValidator : AbstractValidator<EditDeliveryApprovalHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditDeliveryApprovalHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DeliveryApprovalHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("DeliveryApprovalHistory with id {PropertyValue} does not exists");
        
    }
}
