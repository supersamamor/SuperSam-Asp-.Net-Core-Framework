using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

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
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Edit(request, cancellationToken));
}

public class EditMainModulePlaceHolderCommandValidator : AbstractValidator<EditMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditMainModulePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MainModulePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModulePlaceHolder with id {PropertyValue} does not exists");
        Template:[InsertNewUniqueValidationFromEditCommandTextHere]
    }
}
