using AutoMapper;
using CTI.WebAppTemplate.Application.Common;
using CTI.WebAppTemplate.Core.Inventory;
using CTI.WebAppTemplate.Infrastructure.Data;
using CTI.WebAppTemplate.Infrastructure.Extensions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.WebAppTemplate.Application.Features.Inventory.Projects.Commands;

public record AddProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class AddProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, AddProjectCommand>, IRequestHandler<AddProjectCommand, Validation<Error, ProjectState>>
{
    public AddProjectCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(AddProjectCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Add(request, cancellationToken));
}

public class AddProjectCommandValidator : AbstractValidator<AddProjectCommand>
{
    readonly ApplicationContext _context;

    public AddProjectCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectState>(x => x.Id == id, cancellation))
                          .WithMessage("Project with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<ProjectState>(x => x.Name == name, cancellation))
                          .WithMessage("Project with name {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<ProjectState>(x => x.Code == code, cancellation))
                          .WithMessage("Project with code {PropertyValue} already exists");
    }
}
