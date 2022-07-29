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

public record AddTenantPOSCommand : TenantPOSState, IRequest<Validation<Error, TenantPOSState>>;

public class AddTenantPOSCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSState, AddTenantPOSCommand>, IRequestHandler<AddTenantPOSCommand, Validation<Error, TenantPOSState>>
{
    public AddTenantPOSCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTenantPOSCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantPOSState>> Handle(AddTenantPOSCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTenantPOSCommandValidator : AbstractValidator<AddTenantPOSCommand>
{
    readonly ApplicationContext _context;

    public AddTenantPOSCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TenantPOSState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantPOS with id {PropertyValue} already exists");
        
    }
}
