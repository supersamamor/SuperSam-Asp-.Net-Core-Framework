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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Commands;

public record DeleteProjectDeliverableCommand : BaseCommand, IRequest<Validation<Error, ProjectDeliverableState>>;

public class DeleteProjectDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, ProjectDeliverableState, DeleteProjectDeliverableCommand>, IRequestHandler<DeleteProjectDeliverableCommand, Validation<Error, ProjectDeliverableState>>
{
    public DeleteProjectDeliverableCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectDeliverableCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectDeliverableState>> Handle(DeleteProjectDeliverableCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectDeliverableCommandValidator : AbstractValidator<DeleteProjectDeliverableCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectDeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectDeliverable with id {PropertyValue} does not exists");
    }
}
