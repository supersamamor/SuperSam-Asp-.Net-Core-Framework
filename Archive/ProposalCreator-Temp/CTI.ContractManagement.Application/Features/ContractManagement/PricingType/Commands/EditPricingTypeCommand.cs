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

public record EditPricingTypeCommand : PricingTypeState, IRequest<Validation<Error, PricingTypeState>>;

public class EditPricingTypeCommandHandler : BaseCommandHandler<ApplicationContext, PricingTypeState, EditPricingTypeCommand>, IRequestHandler<EditPricingTypeCommand, Validation<Error, PricingTypeState>>
{
    public EditPricingTypeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPricingTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PricingTypeState>> Handle(EditPricingTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPricingTypeCommandValidator : AbstractValidator<EditPricingTypeCommand>
{
    readonly ApplicationContext _context;

    public EditPricingTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PricingTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PricingType with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<PricingTypeState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("PricingType with name {PropertyValue} already exists");
	
    }
}
