using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;

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
