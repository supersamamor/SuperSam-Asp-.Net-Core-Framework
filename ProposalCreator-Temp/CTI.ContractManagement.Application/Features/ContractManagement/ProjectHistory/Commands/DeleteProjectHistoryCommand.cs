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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Commands;

public record DeleteProjectHistoryCommand : BaseCommand, IRequest<Validation<Error, ProjectHistoryState>>;

public class DeleteProjectHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectHistoryState, DeleteProjectHistoryCommand>, IRequestHandler<DeleteProjectHistoryCommand, Validation<Error, ProjectHistoryState>>
{
    public DeleteProjectHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectHistoryState>> Handle(DeleteProjectHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectHistoryCommandValidator : AbstractValidator<DeleteProjectHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectHistory with id {PropertyValue} does not exists");
    }
}
