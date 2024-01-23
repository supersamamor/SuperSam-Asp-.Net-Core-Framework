using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Commands;

public record AddEmployeeCommand : EmployeeState, IRequest<Validation<Error, EmployeeState>>;

public class AddEmployeeCommandHandler : BaseCommandHandler<ApplicationContext, EmployeeState, AddEmployeeCommand>, IRequestHandler<AddEmployeeCommand, Validation<Error, EmployeeState>>
{
	private readonly IdentityContext _identityContext;
    public AddEmployeeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddEmployeeCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, EmployeeState>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddEmployee(request, cancellationToken));


	public async Task<Validation<Error, EmployeeState>> AddEmployee(AddEmployeeCommand request, CancellationToken cancellationToken)
	{
		EmployeeState entity = Mapper.Map<EmployeeState>(request);
		UpdateContactInformationList(entity);
		UpdateHealthDeclarationList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, EmployeeState>(entity);
	}
	
	private void UpdateContactInformationList(EmployeeState entity)
	{
		if (entity.ContactInformationList?.Count > 0)
		{
			foreach (var contactInformation in entity.ContactInformationList!)
			{
				Context.Entry(contactInformation).State = EntityState.Added;
			}
		}
	}
	private void UpdateHealthDeclarationList(EmployeeState entity)
	{
		if (entity.HealthDeclarationList?.Count > 0)
		{
			foreach (var healthDeclaration in entity.HealthDeclarationList!)
			{
				Context.Entry(healthDeclaration).State = EntityState.Added;
			}
		}
	}
	
}

public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
{
    readonly ApplicationContext _context;

    public AddEmployeeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<EmployeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Employee with id {PropertyValue} already exists");
        RuleFor(x => x.EmployeeCode).MustAsync(async (employeeCode, cancellation) => await _context.NotExists<EmployeeState>(x => x.EmployeeCode == employeeCode, cancellationToken: cancellation)).WithMessage("Employee with employeeCode {PropertyValue} already exists");
	
    }
}
