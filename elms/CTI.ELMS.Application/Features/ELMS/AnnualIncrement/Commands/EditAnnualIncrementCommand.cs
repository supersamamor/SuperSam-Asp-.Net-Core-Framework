using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrement.Commands;

public record EditAnnualIncrementCommand : AnnualIncrementState, IRequest<Validation<Error, AnnualIncrementState>>;

public class EditAnnualIncrementCommandHandler : BaseCommandHandler<ApplicationContext, AnnualIncrementState, EditAnnualIncrementCommand>, IRequestHandler<EditAnnualIncrementCommand, Validation<Error, AnnualIncrementState>>
{
    public EditAnnualIncrementCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditAnnualIncrementCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, AnnualIncrementState>> Handle(EditAnnualIncrementCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditAnnualIncrementCommandValidator : AbstractValidator<EditAnnualIncrementCommand>
{
    readonly ApplicationContext _context;

    public EditAnnualIncrementCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<AnnualIncrementState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("AnnualIncrement with id {PropertyValue} does not exists");
        
    }
}
