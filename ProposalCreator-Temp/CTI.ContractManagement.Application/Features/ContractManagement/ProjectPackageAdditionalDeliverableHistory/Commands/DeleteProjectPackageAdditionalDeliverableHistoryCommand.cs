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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Commands;

public record DeleteProjectPackageAdditionalDeliverableHistoryCommand : BaseCommand, IRequest<Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>>;

public class DeleteProjectPackageAdditionalDeliverableHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageAdditionalDeliverableHistoryState, DeleteProjectPackageAdditionalDeliverableHistoryCommand>, IRequestHandler<DeleteProjectPackageAdditionalDeliverableHistoryCommand, Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>>
{
    public DeleteProjectPackageAdditionalDeliverableHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectPackageAdditionalDeliverableHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>> Handle(DeleteProjectPackageAdditionalDeliverableHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectPackageAdditionalDeliverableHistoryCommandValidator : AbstractValidator<DeleteProjectPackageAdditionalDeliverableHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectPackageAdditionalDeliverableHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageAdditionalDeliverableHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageAdditionalDeliverableHistory with id {PropertyValue} does not exists");
    }
}
