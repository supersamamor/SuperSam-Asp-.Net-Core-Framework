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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Commands;

public record DeleteModuleNamePlaceHolderCommand : BaseCommand, IRequest<Validation<Error, ModuleNamePlaceHolderState>>;

public class DeleteModuleNamePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, ModuleNamePlaceHolderState, DeleteModuleNamePlaceHolderCommand>, IRequestHandler<DeleteModuleNamePlaceHolderCommand, Validation<Error, ModuleNamePlaceHolderState>>
{
    public DeleteModuleNamePlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteModuleNamePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ModuleNamePlaceHolderState>> Handle(DeleteModuleNamePlaceHolderCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteModuleNamePlaceHolderCommandValidator : AbstractValidator<DeleteModuleNamePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public DeleteModuleNamePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ModuleNamePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ModuleNamePlaceHolder with id {PropertyValue} does not exists");
    }
}
