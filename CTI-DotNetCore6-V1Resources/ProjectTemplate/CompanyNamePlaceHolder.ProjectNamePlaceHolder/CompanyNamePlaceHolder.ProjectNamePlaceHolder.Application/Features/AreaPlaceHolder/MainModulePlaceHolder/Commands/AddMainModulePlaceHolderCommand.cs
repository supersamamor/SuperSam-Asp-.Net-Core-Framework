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

public record AddMainModulePlaceHolderCommand : MainModulePlaceHolderState, IRequest<Validation<Error, MainModulePlaceHolderState>>;

public class AddMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, MainModulePlaceHolderState, AddMainModulePlaceHolderCommand>, IRequestHandler<AddMainModulePlaceHolderCommand, Validation<Error, MainModulePlaceHolderState>>
{
    public AddMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddMainModulePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModulePlaceHolderState>> Handle(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Add(request, cancellationToken));
}

public class AddMainModulePlaceHolderCommandValidator : AbstractValidator<AddMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddMainModulePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<MainModulePlaceHolderState>(x => x.Id == id, cancellation))
                          .WithMessage("MainModulePlaceHolder with id {PropertyValue} already exists");
		Template:[InsertNewUniqueValidationFromCommandTextHere]        
    }
}
