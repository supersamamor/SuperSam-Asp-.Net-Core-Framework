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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Commands;

public record AddModuleNamePlaceHolderCommand : ModuleNamePlaceHolderState, IRequest<Validation<Error, ModuleNamePlaceHolderState>>;

public class AddModuleNamePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, ModuleNamePlaceHolderState, AddModuleNamePlaceHolderCommand>, IRequestHandler<AddModuleNamePlaceHolderCommand, Validation<Error, ModuleNamePlaceHolderState>>
{
    public AddModuleNamePlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddModuleNamePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ModuleNamePlaceHolderState>> Handle(AddModuleNamePlaceHolderCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddModuleNamePlaceHolderCommandValidator : AbstractValidator<AddModuleNamePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddModuleNamePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ModuleNamePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ModuleNamePlaceHolder with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<ModuleNamePlaceHolderState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("ModuleNamePlaceHolder with code {PropertyValue} already exists");
	
    }
}
