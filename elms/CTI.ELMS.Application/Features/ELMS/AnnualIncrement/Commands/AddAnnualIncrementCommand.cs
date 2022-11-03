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

public record AddAnnualIncrementCommand : AnnualIncrementState, IRequest<Validation<Error, AnnualIncrementState>>;

public class AddAnnualIncrementCommandHandler : BaseCommandHandler<ApplicationContext, AnnualIncrementState, AddAnnualIncrementCommand>, IRequestHandler<AddAnnualIncrementCommand, Validation<Error, AnnualIncrementState>>
{
	private readonly IdentityContext _identityContext;
    public AddAnnualIncrementCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddAnnualIncrementCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, AnnualIncrementState>> Handle(AddAnnualIncrementCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddAnnualIncrementCommandValidator : AbstractValidator<AddAnnualIncrementCommand>
{
    readonly ApplicationContext _context;

    public AddAnnualIncrementCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<AnnualIncrementState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("AnnualIncrement with id {PropertyValue} already exists");
        
    }
}
