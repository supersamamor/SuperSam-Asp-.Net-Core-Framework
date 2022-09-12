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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Commands;

public record DeleteProjectPackageHistoryCommand : BaseCommand, IRequest<Validation<Error, ProjectPackageHistoryState>>;

public class DeleteProjectPackageHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageHistoryState, DeleteProjectPackageHistoryCommand>, IRequestHandler<DeleteProjectPackageHistoryCommand, Validation<Error, ProjectPackageHistoryState>>
{
    public DeleteProjectPackageHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectPackageHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectPackageHistoryState>> Handle(DeleteProjectPackageHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectPackageHistoryCommandValidator : AbstractValidator<DeleteProjectPackageHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectPackageHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageHistory with id {PropertyValue} does not exists");
    }
}
