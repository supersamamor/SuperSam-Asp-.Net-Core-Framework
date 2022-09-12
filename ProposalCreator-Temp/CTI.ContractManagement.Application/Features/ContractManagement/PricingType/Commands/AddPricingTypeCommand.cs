using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ContractManagement.Application.Features.ContractManagement.PricingType.Commands;

public record AddPricingTypeCommand : PricingTypeState, IRequest<Validation<Error, PricingTypeState>>;

public class AddPricingTypeCommandHandler : BaseCommandHandler<ApplicationContext, PricingTypeState, AddPricingTypeCommand>, IRequestHandler<AddPricingTypeCommand, Validation<Error, PricingTypeState>>
{
	private readonly IdentityContext _identityContext;
    public AddPricingTypeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPricingTypeCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PricingTypeState>> Handle(AddPricingTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPricingTypeCommandValidator : AbstractValidator<AddPricingTypeCommand>
{
    readonly ApplicationContext _context;

    public AddPricingTypeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PricingTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PricingType with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<PricingTypeState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("PricingType with name {PropertyValue} already exists");
	
    }
}
