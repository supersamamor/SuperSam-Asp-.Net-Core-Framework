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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;

public record DeleteMainModulePlaceHolderCommand : BaseCommand, IRequest<Validation<Error, MainModulePlaceHolderState>>;

public class DeleteMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, MainModulePlaceHolderState, DeleteMainModulePlaceHolderCommand>, IRequestHandler<DeleteMainModulePlaceHolderCommand, Validation<Error, MainModulePlaceHolderState>>
{
    public DeleteMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteMainModulePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModulePlaceHolderState>> Handle(DeleteMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteMainModulePlaceHolderCommandValidator : AbstractValidator<DeleteMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public DeleteMainModulePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MainModulePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModulePlaceHolder with id {PropertyValue} does not exists");
    }
}
