using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Commands;

public record DeleteDeliverableCommand : BaseCommand, IRequest<Validation<Error, DeliverableState>>;

public class DeleteDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, DeliverableState, DeleteDeliverableCommand>, IRequestHandler<DeleteDeliverableCommand, Validation<Error, DeliverableState>>
{
    public DeleteDeliverableCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteDeliverableCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DeliverableState>> Handle(DeleteDeliverableCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteDeliverableCommandValidator : AbstractValidator<DeleteDeliverableCommand>
{
    readonly ApplicationContext _context;

    public DeleteDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Deliverable with id {PropertyValue} does not exists");
    }
}
