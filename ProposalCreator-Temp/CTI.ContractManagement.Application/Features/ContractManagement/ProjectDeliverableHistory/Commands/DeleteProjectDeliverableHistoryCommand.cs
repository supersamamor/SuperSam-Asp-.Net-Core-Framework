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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Commands;

public record DeleteProjectDeliverableHistoryCommand : BaseCommand, IRequest<Validation<Error, ProjectDeliverableHistoryState>>;

public class DeleteProjectDeliverableHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectDeliverableHistoryState, DeleteProjectDeliverableHistoryCommand>, IRequestHandler<DeleteProjectDeliverableHistoryCommand, Validation<Error, ProjectDeliverableHistoryState>>
{
    public DeleteProjectDeliverableHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectDeliverableHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectDeliverableHistoryState>> Handle(DeleteProjectDeliverableHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectDeliverableHistoryCommandValidator : AbstractValidator<DeleteProjectDeliverableHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectDeliverableHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectDeliverableHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectDeliverableHistory with id {PropertyValue} does not exists");
    }
}
