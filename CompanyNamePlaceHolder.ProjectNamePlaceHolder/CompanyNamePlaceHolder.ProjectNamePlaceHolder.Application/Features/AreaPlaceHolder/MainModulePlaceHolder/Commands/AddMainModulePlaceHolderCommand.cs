using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;

public record AddMainModulePlaceHolderCommand : MainModulePlaceHolderState, IRequest<Validation<Error, MainModulePlaceHolderState>>;

public class AddMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, MainModulePlaceHolderState, AddMainModulePlaceHolderCommand>, IRequestHandler<AddMainModulePlaceHolderCommand, Validation<Error, MainModulePlaceHolderState>>
{
	private readonly IdentityContext _identityContext;
    public AddMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddMainModulePlaceHolderCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, MainModulePlaceHolderState>> Handle(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddMainModulePlaceHolderCommandValidator : AbstractValidator<AddMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddMainModulePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<MainModulePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModulePlaceHolder with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<MainModulePlaceHolderState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("MainModulePlaceHolder with code {PropertyValue} already exists");
	
    }
}
