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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOS.Commands;

public record EditTenantPOSCommand : TenantPOSState, IRequest<Validation<Error, TenantPOSState>>;

public class EditTenantPOSCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSState, EditTenantPOSCommand>, IRequestHandler<EditTenantPOSCommand, Validation<Error, TenantPOSState>>
{
    public EditTenantPOSCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTenantPOSCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantPOSState>> Handle(EditTenantPOSCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTenantPOSCommandValidator : AbstractValidator<EditTenantPOSCommand>
{
    readonly ApplicationContext _context;

    public EditTenantPOSCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantPOSState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantPOS with id {PropertyValue} does not exists");
        
    }
}
