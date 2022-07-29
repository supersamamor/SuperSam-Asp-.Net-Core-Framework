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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;

public record EditTenantPOSSalesCommand : TenantPOSSalesState, IRequest<Validation<Error, TenantPOSSalesState>>;

public class EditTenantPOSSalesCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSSalesState, EditTenantPOSSalesCommand>, IRequestHandler<EditTenantPOSSalesCommand, Validation<Error, TenantPOSSalesState>>
{
    public EditTenantPOSSalesCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTenantPOSSalesCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantPOSSalesState>> Handle(EditTenantPOSSalesCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTenantPOSSalesCommandValidator : AbstractValidator<EditTenantPOSSalesCommand>
{
    readonly ApplicationContext _context;

    public EditTenantPOSSalesCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantPOSSalesState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantPOSSales with id {PropertyValue} does not exists");
        
    }
}
