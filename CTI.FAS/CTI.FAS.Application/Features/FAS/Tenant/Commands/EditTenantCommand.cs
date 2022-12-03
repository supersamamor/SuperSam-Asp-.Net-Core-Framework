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

public record EditTenantCommand : TenantState, IRequest<Validation<Error, TenantState>>;

public class EditTenantCommandHandler : BaseCommandHandler<ApplicationContext, TenantState, EditTenantCommand>, IRequestHandler<EditTenantCommand, Validation<Error, TenantState>>
{
    public EditTenantCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTenantCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantState>> Handle(EditTenantCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTenantCommandValidator : AbstractValidator<EditTenantCommand>
{
    readonly ApplicationContext _context;

    public EditTenantCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Tenant with id {PropertyValue} does not exists");
        
    }
}
