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

public record AddTnamePlaceHolderCommand : TnamePlaceHolderState, IRequest<Validation<Error, TnamePlaceHolderState>>;

public class AddTnamePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, TnamePlaceHolderState, AddTnamePlaceHolderCommand>, IRequestHandler<AddTnamePlaceHolderCommand, Validation<Error, TnamePlaceHolderState>>
{
	private readonly IdentityContext _identityContext;
    public AddTnamePlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTnamePlaceHolderCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, TnamePlaceHolderState>> Handle(AddTnamePlaceHolderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTnamePlaceHolderCommandValidator : AbstractValidator<AddTnamePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddTnamePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TnamePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TnamePlaceHolder with id {PropertyValue} already exists");
        RuleFor(x => x.Colname).MustAsync(async (colname, cancellation) => await _context.NotExists<TnamePlaceHolderState>(x => x.Colname == colname, cancellationToken: cancellation)).WithMessage("TnamePlaceHolder with colname {PropertyValue} already exists");
	
    }
}
