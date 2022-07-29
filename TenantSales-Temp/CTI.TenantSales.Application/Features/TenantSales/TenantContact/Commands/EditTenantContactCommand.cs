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

public record EditTenantContactCommand : TenantContactState, IRequest<Validation<Error, TenantContactState>>;

public class EditTenantContactCommandHandler : BaseCommandHandler<ApplicationContext, TenantContactState, EditTenantContactCommand>, IRequestHandler<EditTenantContactCommand, Validation<Error, TenantContactState>>
{
    public EditTenantContactCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTenantContactCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantContactState>> Handle(EditTenantContactCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTenantContactCommandValidator : AbstractValidator<EditTenantContactCommand>
{
    readonly ApplicationContext _context;

    public EditTenantContactCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantContactState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantContact with id {PropertyValue} does not exists");
        
    }
}
