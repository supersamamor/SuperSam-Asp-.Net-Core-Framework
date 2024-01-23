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

public record EditEmployeeCommand : EmployeeState, IRequest<Validation<Error, EmployeeState>>;

public class EditEmployeeCommandHandler : BaseCommandHandler<ApplicationContext, EmployeeState, EditEmployeeCommand>, IRequestHandler<EditEmployeeCommand, Validation<Error, EmployeeState>>
{
    public EditEmployeeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditEmployeeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, EmployeeState>> Handle(EditEmployeeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditEmployee(request, cancellationToken));


	public async Task<Validation<Error, EmployeeState>> EditEmployee(EditEmployeeCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Employee.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateContactInformationList(entity, request, cancellationToken);
		await UpdateHealthDeclarationList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, EmployeeState>(entity);
	}
	
	private async Task UpdateContactInformationList(EmployeeState entity, EditEmployeeCommand request, CancellationToken cancellationToken)
	{
		IList<ContactInformationState> contactInformationListForDeletion = new List<ContactInformationState>();
		var queryContactInformationForDeletion = Context.ContactInformation.Where(l => l.EmployeeId == request.Id).AsNoTracking();
		if (entity.ContactInformationList?.Count > 0)
		{
			queryContactInformationForDeletion = queryContactInformationForDeletion.Where(l => !(entity.ContactInformationList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		contactInformationListForDeletion = await queryContactInformationForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var contactInformation in contactInformationListForDeletion!)
		{
			Context.Entry(contactInformation).State = EntityState.Deleted;
		}
		if (entity.ContactInformationList?.Count > 0)
		{
			foreach (var contactInformation in entity.ContactInformationList.Where(l => !contactInformationListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ContactInformationState>(x => x.Id == contactInformation.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(contactInformation).State = EntityState.Added;
				}
				else
				{
					Context.Entry(contactInformation).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateHealthDeclarationList(EmployeeState entity, EditEmployeeCommand request, CancellationToken cancellationToken)
	{
		IList<HealthDeclarationState> healthDeclarationListForDeletion = new List<HealthDeclarationState>();
		var queryHealthDeclarationForDeletion = Context.HealthDeclaration.Where(l => l.EmployeeId == request.Id).AsNoTracking();
		if (entity.HealthDeclarationList?.Count > 0)
		{
			queryHealthDeclarationForDeletion = queryHealthDeclarationForDeletion.Where(l => !(entity.HealthDeclarationList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		healthDeclarationListForDeletion = await queryHealthDeclarationForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var healthDeclaration in healthDeclarationListForDeletion!)
		{
			Context.Entry(healthDeclaration).State = EntityState.Deleted;
		}
		if (entity.HealthDeclarationList?.Count > 0)
		{
			foreach (var healthDeclaration in entity.HealthDeclarationList.Where(l => !healthDeclarationListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<HealthDeclarationState>(x => x.Id == healthDeclaration.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(healthDeclaration).State = EntityState.Added;
				}
				else
				{
					Context.Entry(healthDeclaration).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditEmployeeCommandValidator : AbstractValidator<EditEmployeeCommand>
{
    readonly ApplicationContext _context;

    public EditEmployeeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EmployeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Employee with id {PropertyValue} does not exists");
        RuleFor(x => x.EmployeeCode).MustAsync(async (request, employeeCode, cancellation) => await _context.NotExists<EmployeeState>(x => x.EmployeeCode == employeeCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Employee with employeeCode {PropertyValue} already exists");
	
    }
}
