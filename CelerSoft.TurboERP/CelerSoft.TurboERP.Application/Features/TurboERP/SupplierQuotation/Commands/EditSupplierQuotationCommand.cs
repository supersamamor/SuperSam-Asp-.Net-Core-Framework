using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Commands;

public record EditSupplierQuotationCommand : SupplierQuotationState, IRequest<Validation<Error, SupplierQuotationState>>;

public class EditSupplierQuotationCommandHandler : BaseCommandHandler<ApplicationContext, SupplierQuotationState, EditSupplierQuotationCommand>, IRequestHandler<EditSupplierQuotationCommand, Validation<Error, SupplierQuotationState>>
{
    public EditSupplierQuotationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSupplierQuotationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SupplierQuotationState>> Handle(EditSupplierQuotationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditSupplierQuotation(request, cancellationToken));


	public async Task<Validation<Error, SupplierQuotationState>> EditSupplierQuotation(EditSupplierQuotationCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.SupplierQuotation.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateSupplierQuotationItemList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SupplierQuotationState>(entity);
	}
	
	private async Task UpdateSupplierQuotationItemList(SupplierQuotationState entity, EditSupplierQuotationCommand request, CancellationToken cancellationToken)
	{
		IList<SupplierQuotationItemState> supplierQuotationItemListForDeletion = new List<SupplierQuotationItemState>();
		var querySupplierQuotationItemForDeletion = Context.SupplierQuotationItem.Where(l => l.SupplierQuotationId == request.Id).AsNoTracking();
		if (entity.SupplierQuotationItemList?.Count > 0)
		{
			querySupplierQuotationItemForDeletion = querySupplierQuotationItemForDeletion.Where(l => !(entity.SupplierQuotationItemList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		supplierQuotationItemListForDeletion = await querySupplierQuotationItemForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var supplierQuotationItem in supplierQuotationItemListForDeletion!)
		{
			Context.Entry(supplierQuotationItem).State = EntityState.Deleted;
		}
		if (entity.SupplierQuotationItemList?.Count > 0)
		{
			foreach (var supplierQuotationItem in entity.SupplierQuotationItemList.Where(l => !supplierQuotationItemListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<SupplierQuotationItemState>(x => x.Id == supplierQuotationItem.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(supplierQuotationItem).State = EntityState.Added;
				}
				else
				{
					Context.Entry(supplierQuotationItem).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditSupplierQuotationCommandValidator : AbstractValidator<EditSupplierQuotationCommand>
{
    readonly ApplicationContext _context;

    public EditSupplierQuotationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierQuotationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierQuotation with id {PropertyValue} does not exists");
        
    }
}
