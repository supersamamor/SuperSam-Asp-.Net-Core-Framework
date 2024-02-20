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

namespace CTI.ContractManagement.Application.Features.ContractManagement.PricingType.Commands;

public record DeletePricingTypeCommand : BaseCommand, IRequest<Validation<Error, PricingTypeState>>;

public class DeletePricingTypeCommandHandler : BaseCommandHandler<ApplicationContext, PricingTypeState, DeletePricingTypeCommand>, IRequestHandler<DeletePricingTypeCommand, Validation<Error, PricingTypeState>>
{
    public DeletePricingTypeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePricingTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PricingTypeState>> Handle(DeletePricingTypeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePricingTypeCommandValidator : AbstractValidator<DeletePricingTypeCommand>
{
    readonly ApplicationContext _context;

    public DeletePricingTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PricingTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PricingType with id {PropertyValue} does not exists");
    }
}
