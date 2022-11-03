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

namespace CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Commands;

public record AddPPlusConnectionSetupCommand : PPlusConnectionSetupState, IRequest<Validation<Error, PPlusConnectionSetupState>>;

public class AddPPlusConnectionSetupCommandHandler : BaseCommandHandler<ApplicationContext, PPlusConnectionSetupState, AddPPlusConnectionSetupCommand>, IRequestHandler<AddPPlusConnectionSetupCommand, Validation<Error, PPlusConnectionSetupState>>
{
	private readonly IdentityContext _identityContext;
    public AddPPlusConnectionSetupCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPPlusConnectionSetupCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PPlusConnectionSetupState>> Handle(AddPPlusConnectionSetupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPPlusConnectionSetupCommandValidator : AbstractValidator<AddPPlusConnectionSetupCommand>
{
    readonly ApplicationContext _context;

    public AddPPlusConnectionSetupCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PPlusConnectionSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PPlusConnectionSetup with id {PropertyValue} already exists");
        RuleFor(x => x.PPlusVersionName).MustAsync(async (pPlusVersionName, cancellation) => await _context.NotExists<PPlusConnectionSetupState>(x => x.PPlusVersionName == pPlusVersionName, cancellationToken: cancellation)).WithMessage("PPlusConnectionSetup with pPlusVersionName {PropertyValue} already exists");
	
    }
}
