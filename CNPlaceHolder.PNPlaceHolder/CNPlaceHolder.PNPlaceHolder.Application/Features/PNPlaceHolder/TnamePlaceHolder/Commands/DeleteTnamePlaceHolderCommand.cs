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

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;

public record DeleteTnamePlaceHolderCommand : BaseCommand, IRequest<Validation<Error, TnamePlaceHolderState>>;

public class DeleteTnamePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, TnamePlaceHolderState, DeleteTnamePlaceHolderCommand>, IRequestHandler<DeleteTnamePlaceHolderCommand, Validation<Error, TnamePlaceHolderState>>
{
    public DeleteTnamePlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTnamePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TnamePlaceHolderState>> Handle(DeleteTnamePlaceHolderCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTnamePlaceHolderCommandValidator : AbstractValidator<DeleteTnamePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public DeleteTnamePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TnamePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TnamePlaceHolder with id {PropertyValue} does not exists");
    }
}
