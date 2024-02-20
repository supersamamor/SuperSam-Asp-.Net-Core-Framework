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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverable.Commands;

public record DeleteProjectPackageAdditionalDeliverableCommand : BaseCommand, IRequest<Validation<Error, ProjectPackageAdditionalDeliverableState>>;

public class DeleteProjectPackageAdditionalDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageAdditionalDeliverableState, DeleteProjectPackageAdditionalDeliverableCommand>, IRequestHandler<DeleteProjectPackageAdditionalDeliverableCommand, Validation<Error, ProjectPackageAdditionalDeliverableState>>
{
    public DeleteProjectPackageAdditionalDeliverableCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectPackageAdditionalDeliverableCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectPackageAdditionalDeliverableState>> Handle(DeleteProjectPackageAdditionalDeliverableCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectPackageAdditionalDeliverableCommandValidator : AbstractValidator<DeleteProjectPackageAdditionalDeliverableCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectPackageAdditionalDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageAdditionalDeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageAdditionalDeliverable with id {PropertyValue} does not exists");
    }
}
