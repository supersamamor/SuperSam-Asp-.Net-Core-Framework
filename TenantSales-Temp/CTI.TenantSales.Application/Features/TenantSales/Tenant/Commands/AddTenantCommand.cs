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

namespace CTI.TenantSales.Application.Features.TenantSales.Tenant.Commands;

public record AddTenantCommand : TenantState, IRequest<Validation<Error, TenantState>>;

public class AddTenantCommandHandler : BaseCommandHandler<ApplicationContext, TenantState, AddTenantCommand>, IRequestHandler<AddTenantCommand, Validation<Error, TenantState>>
{
    public AddTenantCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTenantCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TenantState>> Handle(AddTenantCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddTenant(request, cancellationToken));


	public async Task<Validation<Error, TenantState>> AddTenant(AddTenantCommand request, CancellationToken cancellationToken)
	{
		TenantState entity = Mapper.Map<TenantState>(request);
		UpdateTenantLotList(entity);
		UpdateSalesCategoryList(entity);
		UpdateTenantContactList(entity);
		UpdateTenantPOSList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TenantState>(entity);
	}
	
	private void UpdateTenantLotList(TenantState entity)
	{
		if (entity.TenantLotList?.Count > 0)
		{
			foreach (var tenantLot in entity.TenantLotList!)
			{
				Context.Entry(tenantLot).State = EntityState.Added;
			}
		}
	}
	private void UpdateSalesCategoryList(TenantState entity)
	{
		if (entity.SalesCategoryList?.Count > 0)
		{
			foreach (var salesCategory in entity.SalesCategoryList!)
			{
				Context.Entry(salesCategory).State = EntityState.Added;
			}
		}
	}
	private void UpdateTenantContactList(TenantState entity)
	{
		if (entity.TenantContactList?.Count > 0)
		{
			foreach (var tenantContact in entity.TenantContactList!)
			{
				Context.Entry(tenantContact).State = EntityState.Added;
			}
		}
	}
	private void UpdateTenantPOSList(TenantState entity)
	{
		if (entity.TenantPOSList?.Count > 0)
		{
			foreach (var tenantPOS in entity.TenantPOSList!)
			{
				Context.Entry(tenantPOS).State = EntityState.Added;
			}
		}
	}
	
	
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
