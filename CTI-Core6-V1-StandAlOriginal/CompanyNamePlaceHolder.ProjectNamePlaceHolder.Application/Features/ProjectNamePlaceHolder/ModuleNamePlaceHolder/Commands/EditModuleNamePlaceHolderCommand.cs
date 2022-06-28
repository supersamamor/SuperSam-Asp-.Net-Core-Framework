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

public record EditModuleNamePlaceHolderCommand : ModuleNamePlaceHolderState, IRequest<Validation<Error, ModuleNamePlaceHolderState>>;

public class EditModuleNamePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, ModuleNamePlaceHolderState, EditModuleNamePlaceHolderCommand>, IRequestHandler<EditModuleNamePlaceHolderCommand, Validation<Error, ModuleNamePlaceHolderState>>
{
    public EditModuleNamePlaceHolderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditModuleNamePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ModuleNamePlaceHolderState>> Handle(EditModuleNamePlaceHolderCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditModuleNamePlaceHolderCommandValidator : AbstractValidator<EditModuleNamePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditModuleNamePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ModuleNamePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ModuleNamePlaceHolder with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<ModuleNamePlaceHolderState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ModuleNamePlaceHolder with code {PropertyValue} already exists");
	
    }
}
