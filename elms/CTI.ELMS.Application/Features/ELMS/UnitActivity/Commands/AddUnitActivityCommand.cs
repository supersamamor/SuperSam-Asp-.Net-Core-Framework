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

namespace CTI.ELMS.Application.Features.ELMS.UnitActivity.Commands;

public record AddUnitActivityCommand : UnitActivityState, IRequest<Validation<Error, UnitActivityState>>;

public class AddUnitActivityCommandHandler : BaseCommandHandler<ApplicationContext, UnitActivityState, AddUnitActivityCommand>, IRequestHandler<AddUnitActivityCommand, Validation<Error, UnitActivityState>>
{
	private readonly IdentityContext _identityContext;
    public AddUnitActivityCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUnitActivityCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UnitActivityState>> Handle(AddUnitActivityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddUnitActivityCommandValidator : AbstractValidator<AddUnitActivityCommand>
{
    readonly ApplicationContext _context;

    public AddUnitActivityCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UnitActivityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitActivity with id {PropertyValue} already exists");
        
    }
}
