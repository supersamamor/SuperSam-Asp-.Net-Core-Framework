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

namespace CTI.ELMS.Application.Features.ELMS.BusinessNature.Commands;

public record AddBusinessNatureCommand : BusinessNatureState, IRequest<Validation<Error, BusinessNatureState>>;

public class AddBusinessNatureCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureState, AddBusinessNatureCommand>, IRequestHandler<AddBusinessNatureCommand, Validation<Error, BusinessNatureState>>
{
	private readonly IdentityContext _identityContext;
    public AddBusinessNatureCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBusinessNatureCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, BusinessNatureState>> Handle(AddBusinessNatureCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddBusinessNatureCommandValidator : AbstractValidator<AddBusinessNatureCommand>
{
    readonly ApplicationContext _context;

    public AddBusinessNatureCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BusinessNatureState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNature with id {PropertyValue} already exists");
        RuleFor(x => x.BusinessNatureName).MustAsync(async (businessNatureName, cancellation) => await _context.NotExists<BusinessNatureState>(x => x.BusinessNatureName == businessNatureName, cancellationToken: cancellation)).WithMessage("BusinessNature with businessNatureName {PropertyValue} already exists");
	RuleFor(x => x.BusinessNatureCode).MustAsync(async (businessNatureCode, cancellation) => await _context.NotExists<BusinessNatureState>(x => x.BusinessNatureCode == businessNatureCode, cancellationToken: cancellation)).WithMessage("BusinessNature with businessNatureCode {PropertyValue} already exists");
	
    }
}
