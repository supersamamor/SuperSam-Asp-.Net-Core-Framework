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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Commands;

public record EditSupplierCommand : SupplierState, IRequest<Validation<Error, SupplierState>>;

public class EditSupplierCommandHandler : BaseCommandHandler<ApplicationContext, SupplierState, EditSupplierCommand>, IRequestHandler<EditSupplierCommand, Validation<Error, SupplierState>>
{
    public EditSupplierCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSupplierCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SupplierState>> Handle(EditSupplierCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditSupplier(request, cancellationToken));


	public async Task<Validation<Error, SupplierState>> EditSupplier(EditSupplierCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Supplier.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateSupplierContactPersonList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SupplierState>(entity);
	}
	
	private async Task UpdateSupplierContactPersonList(SupplierState entity, EditSupplierCommand request, CancellationToken cancellationToken)
	{
		IList<SupplierContactPersonState> supplierContactPersonListForDeletion = new List<SupplierContactPersonState>();
		var querySupplierContactPersonForDeletion = Context.SupplierContactPerson.Where(l => l.SupplierId == request.Id).AsNoTracking();
		if (entity.SupplierContactPersonList?.Count > 0)
		{
			querySupplierContactPersonForDeletion = querySupplierContactPersonForDeletion.Where(l => !(entity.SupplierContactPersonList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		supplierContactPersonListForDeletion = await querySupplierContactPersonForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var supplierContactPerson in supplierContactPersonListForDeletion!)
		{
			Context.Entry(supplierContactPerson).State = EntityState.Deleted;
		}
		if (entity.SupplierContactPersonList?.Count > 0)
		{
			foreach (var supplierContactPerson in entity.SupplierContactPersonList.Where(l => !supplierContactPersonListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<SupplierContactPersonState>(x => x.Id == supplierContactPerson.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(supplierContactPerson).State = EntityState.Added;
				}
				else
				{
					Context.Entry(supplierContactPerson).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditSupplierCommandValidator : AbstractValidator<EditSupplierCommand>
{
    readonly ApplicationContext _context;

    public EditSupplierCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Supplier with id {PropertyValue} does not exists");
        RuleFor(x => x.Company).MustAsync(async (request, company, cancellation) => await _context.NotExists<SupplierState>(x => x.Company == company && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Supplier with company {PropertyValue} already exists");
	RuleFor(x => x.TINNumber).MustAsync(async (request, tINNumber, cancellation) => await _context.NotExists<SupplierState>(x => x.TINNumber == tINNumber && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Supplier with tINNumber {PropertyValue} already exists");
	
    }
}
