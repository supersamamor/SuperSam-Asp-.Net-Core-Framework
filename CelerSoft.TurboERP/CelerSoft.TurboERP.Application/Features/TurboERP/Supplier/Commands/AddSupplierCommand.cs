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

public record AddSupplierCommand : SupplierState, IRequest<Validation<Error, SupplierState>>;

public class AddSupplierCommandHandler : BaseCommandHandler<ApplicationContext, SupplierState, AddSupplierCommand>, IRequestHandler<AddSupplierCommand, Validation<Error, SupplierState>>
{
	private readonly IdentityContext _identityContext;
    public AddSupplierCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSupplierCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, SupplierState>> Handle(AddSupplierCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddSupplier(request, cancellationToken));


	public async Task<Validation<Error, SupplierState>> AddSupplier(AddSupplierCommand request, CancellationToken cancellationToken)
	{
		SupplierState entity = Mapper.Map<SupplierState>(request);
		UpdateSupplierContactPersonList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SupplierState>(entity);
	}
	
	private void UpdateSupplierContactPersonList(SupplierState entity)
	{
		if (entity.SupplierContactPersonList?.Count > 0)
		{
			foreach (var supplierContactPerson in entity.SupplierContactPersonList!)
			{
				Context.Entry(supplierContactPerson).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddSupplierCommandValidator : AbstractValidator<AddSupplierCommand>
{
    readonly ApplicationContext _context;

    public AddSupplierCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SupplierState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Supplier with id {PropertyValue} already exists");
        RuleFor(x => x.Company).MustAsync(async (company, cancellation) => await _context.NotExists<SupplierState>(x => x.Company == company, cancellationToken: cancellation)).WithMessage("Supplier with company {PropertyValue} already exists");
	RuleFor(x => x.TINNumber).MustAsync(async (tINNumber, cancellation) => await _context.NotExists<SupplierState>(x => x.TINNumber == tINNumber, cancellationToken: cancellation)).WithMessage("Supplier with tINNumber {PropertyValue} already exists");
	
    }
}
