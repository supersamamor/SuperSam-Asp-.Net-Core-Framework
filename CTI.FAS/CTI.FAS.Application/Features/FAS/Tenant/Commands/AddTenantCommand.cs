using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.Tenant.Commands;

public record AddTenantCommand : TenantState, IRequest<Validation<Error, TenantState>>;

public class AddTenantCommandHandler : BaseCommandHandler<ApplicationContext, TenantState, AddTenantCommand>, IRequestHandler<AddTenantCommand, Validation<Error, TenantState>>
{
	private readonly IdentityContext _identityContext;
    public AddTenantCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTenantCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, TenantState>> Handle(AddTenantCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTenantCommandValidator : AbstractValidator<AddTenantCommand>
{
    readonly ApplicationContext _context;

    public AddTenantCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TenantState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Tenant with id {PropertyValue} already exists");
        
    }
}
