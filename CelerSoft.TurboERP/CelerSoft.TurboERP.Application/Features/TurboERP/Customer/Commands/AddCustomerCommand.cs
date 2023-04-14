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

public record AddCustomerCommand : CustomerState, IRequest<Validation<Error, CustomerState>>;

public class AddCustomerCommandHandler : BaseCommandHandler<ApplicationContext, CustomerState, AddCustomerCommand>, IRequestHandler<AddCustomerCommand, Validation<Error, CustomerState>>
{
	private readonly IdentityContext _identityContext;
    public AddCustomerCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCustomerCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, CustomerState>> Handle(AddCustomerCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddCustomer(request, cancellationToken));


	public async Task<Validation<Error, CustomerState>> AddCustomer(AddCustomerCommand request, CancellationToken cancellationToken)
	{
		CustomerState entity = Mapper.Map<CustomerState>(request);
		UpdateCustomerContactPersonList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CustomerState>(entity);
	}
	
	private void UpdateCustomerContactPersonList(CustomerState entity)
	{
		if (entity.CustomerContactPersonList?.Count > 0)
		{
			foreach (var customerContactPerson in entity.CustomerContactPersonList!)
			{
				Context.Entry(customerContactPerson).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    readonly ApplicationContext _context;

    public AddCustomerCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CustomerState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Customer with id {PropertyValue} already exists");
        RuleFor(x => x.Company).MustAsync(async (company, cancellation) => await _context.NotExists<CustomerState>(x => x.Company == company, cancellationToken: cancellation)).WithMessage("Customer with company {PropertyValue} already exists");
	RuleFor(x => x.TINNumber).MustAsync(async (tINNumber, cancellation) => await _context.NotExists<CustomerState>(x => x.TINNumber == tINNumber, cancellationToken: cancellation)).WithMessage("Customer with tINNumber {PropertyValue} already exists");
	
    }
}
