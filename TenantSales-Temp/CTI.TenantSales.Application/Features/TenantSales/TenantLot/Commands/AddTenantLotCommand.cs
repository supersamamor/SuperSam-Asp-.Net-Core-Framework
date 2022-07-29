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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantLot.Commands;

public record AddTenantLotCommand : TenantLotState, IRequest<Validation<Error, TenantLotState>>;

public class AddTenantLotCommandHandler : BaseCommandHandler<ApplicationContext, TenantLotState, AddTenantLotCommand>, IRequestHandler<AddTenantLotCommand, Validation<Error, TenantLotState>>
{
    public AddTenantLotCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTenantLotCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantLotState>> Handle(AddTenantLotCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTenantLotCommandValidator : AbstractValidator<AddTenantLotCommand>
{
    readonly ApplicationContext _context;

    public AddTenantLotCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TenantLotState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantLot with id {PropertyValue} already exists");
        
    }
}
