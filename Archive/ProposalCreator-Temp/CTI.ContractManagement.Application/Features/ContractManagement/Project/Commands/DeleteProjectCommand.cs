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

namespace CTI.ContractManagement.Application.Features.ContractManagement.Project.Commands;

public record DeleteProjectCommand : BaseCommand, IRequest<Validation<Error, ProjectState>>;

public class DeleteProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, DeleteProjectCommand>, IRequestHandler<DeleteProjectCommand, Validation<Error, ProjectState>>
{
    public DeleteProjectCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Project with id {PropertyValue} does not exists");
    }
}
