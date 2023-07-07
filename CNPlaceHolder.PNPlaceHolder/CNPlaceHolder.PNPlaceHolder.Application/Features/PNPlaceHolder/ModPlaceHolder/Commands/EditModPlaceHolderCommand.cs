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

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;

public record EditModPlaceHolderCommand : ModPlaceHolderState, IRequest<Validation<Error, ModPlaceHolderState>>;

public class EditModPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, ModPlaceHolderState, EditModPlaceHolderCommand>, IRequestHandler<EditModPlaceHolderCommand, Validation<Error, ModPlaceHolderState>>
{
    public EditModPlaceHolderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditModPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ModPlaceHolderState>> Handle(EditModPlaceHolderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditModPlaceHolderCommandValidator : AbstractValidator<EditModPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditModPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ModPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ModPlaceHolder with id {PropertyValue} does not exists");
        RuleFor(x => x.ColPlaceHolder).MustAsync(async (request, colPlaceHolder, cancellation) => await _context.NotExists<ModPlaceHolderState>(x => x.ColPlaceHolder == colPlaceHolder && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ModPlaceHolder with colPlaceHolder {PropertyValue} already exists");
	
    }
}
