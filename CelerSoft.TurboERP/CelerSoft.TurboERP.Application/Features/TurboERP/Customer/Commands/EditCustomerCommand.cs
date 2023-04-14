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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Commands;

public record EditCustomerCommand : CustomerState, IRequest<Validation<Error, CustomerState>>;

public class EditCustomerCommandHandler : BaseCommandHandler<ApplicationContext, CustomerState, EditCustomerCommand>, IRequestHandler<EditCustomerCommand, Validation<Error, CustomerState>>
{
    public EditCustomerCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCustomerCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CustomerState>> Handle(EditCustomerCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditCustomer(request, cancellationToken));


	public async Task<Validation<Error, CustomerState>> EditCustomer(EditCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Customer.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateCustomerContactPersonList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CustomerState>(entity);
	}
	
	private async Task UpdateCustomerContactPersonList(CustomerState entity, EditCustomerCommand request, CancellationToken cancellationToken)
	{
		IList<CustomerContactPersonState> customerContactPersonListForDeletion = new List<CustomerContactPersonState>();
		var queryCustomerContactPersonForDeletion = Context.CustomerContactPerson.Where(l => l.CustomerId == request.Id).AsNoTracking();
		if (entity.CustomerContactPersonList?.Count > 0)
		{
			queryCustomerContactPersonForDeletion = queryCustomerContactPersonForDeletion.Where(l => !(entity.CustomerContactPersonList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		customerContactPersonListForDeletion = await queryCustomerContactPersonForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var customerContactPerson in customerContactPersonListForDeletion!)
		{
			Context.Entry(customerContactPerson).State = EntityState.Deleted;
		}
		if (entity.CustomerContactPersonList?.Count > 0)
		{
			foreach (var customerContactPerson in entity.CustomerContactPersonList.Where(l => !customerContactPersonListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<CustomerContactPersonState>(x => x.Id == customerContactPerson.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(customerContactPerson).State = EntityState.Added;
				}
				else
				{
					Context.Entry(customerContactPerson).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditCustomerCommandValidator : AbstractValidator<EditCustomerCommand>
{
    readonly ApplicationContext _context;

    public EditCustomerCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CustomerState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Customer with id {PropertyValue} does not exists");
        RuleFor(x => x.Company).MustAsync(async (request, company, cancellation) => await _context.NotExists<CustomerState>(x => x.Company == company && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Customer with company {PropertyValue} already exists");
	RuleFor(x => x.TINNumber).MustAsync(async (request, tINNumber, cancellation) => await _context.NotExists<CustomerState>(x => x.TINNumber == tINNumber && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Customer with tINNumber {PropertyValue} already exists");
	
    }
}
