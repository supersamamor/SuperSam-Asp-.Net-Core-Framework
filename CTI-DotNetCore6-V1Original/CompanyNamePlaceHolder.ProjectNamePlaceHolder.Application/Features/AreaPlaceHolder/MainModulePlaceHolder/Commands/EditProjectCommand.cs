using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;

public record EditProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class EditProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, EditProjectCommand>, IRequestHandler<EditProjectCommand, Validation<Error, ProjectState>>
{
    public EditProjectCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(EditProjectCommand request, CancellationToken cancellationToken) =>
        await _context.GetSingle<ProjectState>(p => p.Id == request.Id, cancellationToken, true).MatchAsync(
            async project => await UpdateProject(request, project, cancellationToken),
            () => Fail<Error, ProjectState>($"Project with id {request.Id} does not exist"));

    async Task<Validation<Error, ProjectState>> UpdateProject(EditProjectCommand request, ProjectState project, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Edit(request, project, cancellationToken));
}

public class EditProjectCommandValidator : AbstractValidator<EditProjectCommand>
{
    readonly ApplicationContext _context;

    public EditProjectCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<ProjectState>(x => x.Name == name && x.Id != request.Id, cancellation))
                            .WithMessage("Project with name {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<ProjectState>(x => x.Code == code && x.Id != request.Id, cancellation))
                            .WithMessage("Project with code {PropertyValue} already exists");
    }
}
