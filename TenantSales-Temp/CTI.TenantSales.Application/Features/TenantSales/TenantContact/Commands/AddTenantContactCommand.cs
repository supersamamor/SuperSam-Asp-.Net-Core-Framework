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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantContact.Commands;

public record AddTenantContactCommand : TenantContactState, IRequest<Validation<Error, TenantContactState>>;

public class AddTenantContactCommandHandler : BaseCommandHandler<ApplicationContext, TenantContactState, AddTenantContactCommand>, IRequestHandler<AddTenantContactCommand, Validation<Error, TenantContactState>>
{
    public AddTenantContactCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTenantContactCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantContactState>> Handle(AddTenantContactCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTenantContactCommandValidator : AbstractValidator<AddTenantContactCommand>
{
    readonly ApplicationContext _context;

    public AddTenantContactCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TenantContactState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantContact with id {PropertyValue} already exists");
        
    }
}
