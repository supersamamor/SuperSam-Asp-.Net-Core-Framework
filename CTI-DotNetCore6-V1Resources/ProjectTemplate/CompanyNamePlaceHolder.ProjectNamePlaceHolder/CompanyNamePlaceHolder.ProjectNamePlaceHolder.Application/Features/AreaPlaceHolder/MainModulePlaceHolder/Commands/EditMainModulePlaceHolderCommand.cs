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
            async mainModulePlaceHolder => await UpdateMainModulePlaceHolder(request, mainModulePlaceHolder, cancellationToken),
            () => Fail<Error, MainModulePlaceHolderState>($"MainModulePlaceHolder with id {request.Id} does not exist"));

    async Task<Validation<Error, MainModulePlaceHolderState>> UpdateMainModulePlaceHolder(EditMainModulePlaceHolderCommand request, MainModulePlaceHolderState mainModulePlaceHolder, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Edit(request, mainModulePlaceHolder, cancellationToken));
}

public class EditMainModulePlaceHolderCommandValidator : AbstractValidator<EditMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditMainModulePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
        Template:[InsertNewUniqueValidationFromCommandTextHere]
    }
}
