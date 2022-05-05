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

public record EditMainModulePlaceHolderCommand : MainModulePlaceHolderState, IRequest<Validation<Error, MainModulePlaceHolderState>>;

public class EditMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, MainModulePlaceHolderState, EditMainModulePlaceHolderCommand>, IRequestHandler<EditMainModulePlaceHolderCommand, Validation<Error, MainModulePlaceHolderState>>
{
    public EditMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditMainModulePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModulePlaceHolderState>> Handle(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
        await _context.GetSingle<MainModulePlaceHolderState>(p => p.Id == request.Id, cancellationToken, true).MatchAsync(
            async project => await UpdateProject(request, project, cancellationToken),
            () => Fail<Error, MainModulePlaceHolderState>($"Project with id {request.Id} does not exist"));

    async Task<Validation<Error, MainModulePlaceHolderState>> UpdateProject(EditMainModulePlaceHolderCommand request, MainModulePlaceHolderState project, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Edit(request, project, cancellationToken));
}

public class EditProjectCommandValidator : AbstractValidator<EditMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditProjectCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<MainModulePlaceHolderState>(x => x.Code == code && x.Id != request.Id, cancellation))
                            .WithMessage("Project with code {PropertyValue} already exists");
    }
}
