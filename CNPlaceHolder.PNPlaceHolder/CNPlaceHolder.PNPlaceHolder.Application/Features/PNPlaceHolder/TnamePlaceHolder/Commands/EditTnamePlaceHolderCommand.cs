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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;

public record EditTnamePlaceHolderCommand : TnamePlaceHolderState, IRequest<Validation<Error, TnamePlaceHolderState>>;

public class EditTnamePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, TnamePlaceHolderState, EditTnamePlaceHolderCommand>, IRequestHandler<EditTnamePlaceHolderCommand, Validation<Error, TnamePlaceHolderState>>
{
    public EditTnamePlaceHolderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTnamePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TnamePlaceHolderState>> Handle(EditTnamePlaceHolderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTnamePlaceHolderCommandValidator : AbstractValidator<EditTnamePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditTnamePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TnamePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TnamePlaceHolder with id {PropertyValue} does not exists");
        RuleFor(x => x.Colname).MustAsync(async (request, colname, cancellation) => await _context.NotExists<TnamePlaceHolderState>(x => x.Colname == colname && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("TnamePlaceHolder with colname {PropertyValue} already exists");
	
    }
}
