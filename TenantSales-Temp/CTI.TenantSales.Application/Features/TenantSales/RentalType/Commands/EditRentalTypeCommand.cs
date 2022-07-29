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

public record EditRentalTypeCommand : RentalTypeState, IRequest<Validation<Error, RentalTypeState>>;

public class EditRentalTypeCommandHandler : BaseCommandHandler<ApplicationContext, RentalTypeState, EditRentalTypeCommand>, IRequestHandler<EditRentalTypeCommand, Validation<Error, RentalTypeState>>
{
    public EditRentalTypeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditRentalTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, RentalTypeState>> Handle(EditRentalTypeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditRentalTypeCommandValidator : AbstractValidator<EditRentalTypeCommand>
{
    readonly ApplicationContext _context;

    public EditRentalTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RentalTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RentalType with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<RentalTypeState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("RentalType with name {PropertyValue} already exists");
	
    }
}
