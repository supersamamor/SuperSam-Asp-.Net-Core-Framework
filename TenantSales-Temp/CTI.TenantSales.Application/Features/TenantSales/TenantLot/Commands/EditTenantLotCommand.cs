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

public record EditTenantLotCommand : TenantLotState, IRequest<Validation<Error, TenantLotState>>;

public class EditTenantLotCommandHandler : BaseCommandHandler<ApplicationContext, TenantLotState, EditTenantLotCommand>, IRequestHandler<EditTenantLotCommand, Validation<Error, TenantLotState>>
{
    public EditTenantLotCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTenantLotCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantLotState>> Handle(EditTenantLotCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTenantLotCommandValidator : AbstractValidator<EditTenantLotCommand>
{
    readonly ApplicationContext _context;

    public EditTenantLotCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantLotState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantLot with id {PropertyValue} does not exists");
        
    }
}
