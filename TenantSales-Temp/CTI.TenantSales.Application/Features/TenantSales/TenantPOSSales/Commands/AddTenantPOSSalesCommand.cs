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

public record AddTenantPOSSalesCommand : TenantPOSSalesState, IRequest<Validation<Error, TenantPOSSalesState>>;

public class AddTenantPOSSalesCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSSalesState, AddTenantPOSSalesCommand>, IRequestHandler<AddTenantPOSSalesCommand, Validation<Error, TenantPOSSalesState>>
{
    public AddTenantPOSSalesCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTenantPOSSalesCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TenantPOSSalesState>> Handle(AddTenantPOSSalesCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTenantPOSSalesCommandValidator : AbstractValidator<AddTenantPOSSalesCommand>
{
    readonly ApplicationContext _context;

    public AddTenantPOSSalesCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TenantPOSSalesState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantPOSSales with id {PropertyValue} already exists");
        
    }
}
