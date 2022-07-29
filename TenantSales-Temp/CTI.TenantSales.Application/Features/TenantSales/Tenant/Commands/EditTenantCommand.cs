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
			async request => await EditTenant(request, cancellationToken));


	public async Task<Validation<Error, TenantState>> EditTenant(EditTenantCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Tenant.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTenantLotList(entity, request, cancellationToken);
		await UpdateSalesCategoryList(entity, request, cancellationToken);
		await UpdateTenantContactList(entity, request, cancellationToken);
		await UpdateTenantPOSList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TenantState>(entity);
	}
	
	private async Task UpdateTenantLotList(TenantState entity, EditTenantCommand request, CancellationToken cancellationToken)
	{
		IList<TenantLotState> tenantLotListForDeletion = new List<TenantLotState>();
		var queryTenantLotForDeletion = Context.TenantLot.Where(l => l.TenantId == request.Id).AsNoTracking();
		if (entity.TenantLotList?.Count > 0)
		{
			queryTenantLotForDeletion = queryTenantLotForDeletion.Where(l => !(entity.TenantLotList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		tenantLotListForDeletion = await queryTenantLotForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var tenantLot in tenantLotListForDeletion!)
		{
			Context.Entry(tenantLot).State = EntityState.Deleted;
		}
		if (entity.TenantLotList?.Count > 0)
		{
			foreach (var tenantLot in entity.TenantLotList.Where(l => !tenantLotListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TenantLotState>(x => x.Id == tenantLot.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(tenantLot).State = EntityState.Added;
				}
				else
				{
					Context.Entry(tenantLot).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateSalesCategoryList(TenantState entity, EditTenantCommand request, CancellationToken cancellationToken)
	{
		IList<SalesCategoryState> salesCategoryListForDeletion = new List<SalesCategoryState>();
		var querySalesCategoryForDeletion = Context.SalesCategory.Where(l => l.TenantId == request.Id).AsNoTracking();
		if (entity.SalesCategoryList?.Count > 0)
		{
			querySalesCategoryForDeletion = querySalesCategoryForDeletion.Where(l => !(entity.SalesCategoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		salesCategoryListForDeletion = await querySalesCategoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var salesCategory in salesCategoryListForDeletion!)
		{
			Context.Entry(salesCategory).State = EntityState.Deleted;
		}
		if (entity.SalesCategoryList?.Count > 0)
		{
			foreach (var salesCategory in entity.SalesCategoryList.Where(l => !salesCategoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<SalesCategoryState>(x => x.Id == salesCategory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(salesCategory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(salesCategory).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateTenantContactList(TenantState entity, EditTenantCommand request, CancellationToken cancellationToken)
	{
		IList<TenantContactState> tenantContactListForDeletion = new List<TenantContactState>();
		var queryTenantContactForDeletion = Context.TenantContact.Where(l => l.TenantId == request.Id).AsNoTracking();
		if (entity.TenantContactList?.Count > 0)
		{
			queryTenantContactForDeletion = queryTenantContactForDeletion.Where(l => !(entity.TenantContactList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		tenantContactListForDeletion = await queryTenantContactForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var tenantContact in tenantContactListForDeletion!)
		{
			Context.Entry(tenantContact).State = EntityState.Deleted;
		}
		if (entity.TenantContactList?.Count > 0)
		{
			foreach (var tenantContact in entity.TenantContactList.Where(l => !tenantContactListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TenantContactState>(x => x.Id == tenantContact.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(tenantContact).State = EntityState.Added;
				}
				else
				{
					Context.Entry(tenantContact).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateTenantPOSList(TenantState entity, EditTenantCommand request, CancellationToken cancellationToken)
	{
		IList<TenantPOSState> tenantPOSListForDeletion = new List<TenantPOSState>();
		var queryTenantPOSForDeletion = Context.TenantPOS.Where(l => l.TenantId == request.Id).AsNoTracking();
		if (entity.TenantPOSList?.Count > 0)
		{
			queryTenantPOSForDeletion = queryTenantPOSForDeletion.Where(l => !(entity.TenantPOSList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		tenantPOSListForDeletion = await queryTenantPOSForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var tenantPOS in tenantPOSListForDeletion!)
		{
			Context.Entry(tenantPOS).State = EntityState.Deleted;
		}
		if (entity.TenantPOSList?.Count > 0)
		{
			foreach (var tenantPOS in entity.TenantPOSList.Where(l => !tenantPOSListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TenantPOSState>(x => x.Id == tenantPOS.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(tenantPOS).State = EntityState.Added;
				}
				else
				{
					Context.Entry(tenantPOS).State = EntityState.Modified;
				}
			}
		}
	}
	
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
