using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.RentalType.Commands;

public record AddRentalTypeCommand : RentalTypeState, IRequest<Validation<Error, RentalTypeState>>;

public class AddRentalTypeCommandHandler : BaseCommandHandler<ApplicationContext, RentalTypeState, AddRentalTypeCommand>, IRequestHandler<AddRentalTypeCommand, Validation<Error, RentalTypeState>>
{
    public AddRentalTypeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddRentalTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, RentalTypeState>> Handle(AddRentalTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddRentalTypeCommandValidator : AbstractValidator<AddRentalTypeCommand>
{
    readonly ApplicationContext _context;

    public AddRentalTypeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<RentalTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RentalType with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<RentalTypeState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("RentalType with name {PropertyValue} already exists");
	
    }
}
