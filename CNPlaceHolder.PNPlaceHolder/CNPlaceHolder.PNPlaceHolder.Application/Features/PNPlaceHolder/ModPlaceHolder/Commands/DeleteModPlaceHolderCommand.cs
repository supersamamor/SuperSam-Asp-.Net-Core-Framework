using AutoMapper;
using CNPlaceHolder.Common.Core.Commands;
using CNPlaceHolder.Common.Data;
using CNPlaceHolder.Common.Utility.Validators;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;

public record DeleteModPlaceHolderCommand : BaseCommand, IRequest<Validation<Error, ModPlaceHolderState>>;

public class DeleteModPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, ModPlaceHolderState, DeleteModPlaceHolderCommand>, IRequestHandler<DeleteModPlaceHolderCommand, Validation<Error, ModPlaceHolderState>>
{
    public DeleteModPlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteModPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ModPlaceHolderState>> Handle(DeleteModPlaceHolderCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteModPlaceHolderCommandValidator : AbstractValidator<DeleteModPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public DeleteModPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ModPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ModPlaceHolder with id {PropertyValue} does not exists");
    }
}
