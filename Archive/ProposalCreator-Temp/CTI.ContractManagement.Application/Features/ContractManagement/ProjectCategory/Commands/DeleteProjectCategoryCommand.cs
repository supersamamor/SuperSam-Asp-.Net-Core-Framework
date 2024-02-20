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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Commands;

public record DeleteProjectCategoryCommand : BaseCommand, IRequest<Validation<Error, ProjectCategoryState>>;

public class DeleteProjectCategoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectCategoryState, DeleteProjectCategoryCommand>, IRequestHandler<DeleteProjectCategoryCommand, Validation<Error, ProjectCategoryState>>
{
    public DeleteProjectCategoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectCategoryState>> Handle(DeleteProjectCategoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectCategoryCommandValidator : AbstractValidator<DeleteProjectCategoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectCategory with id {PropertyValue} does not exists");
    }
}
