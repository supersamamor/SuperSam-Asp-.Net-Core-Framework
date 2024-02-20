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

namespace CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Commands;

public record DeleteDeliveryApprovalHistoryCommand : BaseCommand, IRequest<Validation<Error, DeliveryApprovalHistoryState>>;

public class DeleteDeliveryApprovalHistoryCommandHandler : BaseCommandHandler<ApplicationContext, DeliveryApprovalHistoryState, DeleteDeliveryApprovalHistoryCommand>, IRequestHandler<DeleteDeliveryApprovalHistoryCommand, Validation<Error, DeliveryApprovalHistoryState>>
{
    public DeleteDeliveryApprovalHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteDeliveryApprovalHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DeliveryApprovalHistoryState>> Handle(DeleteDeliveryApprovalHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteDeliveryApprovalHistoryCommandValidator : AbstractValidator<DeleteDeliveryApprovalHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteDeliveryApprovalHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DeliveryApprovalHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("DeliveryApprovalHistory with id {PropertyValue} does not exists");
    }
}
