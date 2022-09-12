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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Commands;

public record DeleteProjectPackageCommand : BaseCommand, IRequest<Validation<Error, ProjectPackageState>>;

public class DeleteProjectPackageCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageState, DeleteProjectPackageCommand>, IRequestHandler<DeleteProjectPackageCommand, Validation<Error, ProjectPackageState>>
{
    public DeleteProjectPackageCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectPackageCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectPackageState>> Handle(DeleteProjectPackageCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectPackageCommandValidator : AbstractValidator<DeleteProjectPackageCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectPackageCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackage with id {PropertyValue} does not exists");
    }
}
