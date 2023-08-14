using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Company.Commands;

public record EditCompanyCommand : CompanyState, IRequest<Validation<Error, CompanyState>>;

public class EditCompanyCommandHandler : BaseCommandHandler<ApplicationContext, CompanyState, EditCompanyCommand>, IRequestHandler<EditCompanyCommand, Validation<Error, CompanyState>>
{
    public EditCompanyCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCompanyCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CompanyState>> Handle(EditCompanyCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditCompany(request, cancellationToken));


	public async Task<Validation<Error, CompanyState>> EditCompany(EditCompanyCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Company.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateDepartmentList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CompanyState>(entity);
	}
	
	private async Task UpdateDepartmentList(CompanyState entity, EditCompanyCommand request, CancellationToken cancellationToken)
	{
		IList<DepartmentState> departmentListForDeletion = new List<DepartmentState>();
		var queryDepartmentForDeletion = Context.Department.Where(l => l.CompanyCode == request.Id).AsNoTracking();
		if (entity.DepartmentList?.Count > 0)
		{
			queryDepartmentForDeletion = queryDepartmentForDeletion.Where(l => !(entity.DepartmentList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		departmentListForDeletion = await queryDepartmentForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var department in departmentListForDeletion!)
		{
			Context.Entry(department).State = EntityState.Deleted;
		}
		if (entity.DepartmentList?.Count > 0)
		{
			foreach (var department in entity.DepartmentList.Where(l => !departmentListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<DepartmentState>(x => x.Id == department.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(department).State = EntityState.Added;
				}
				else
				{
					Context.Entry(department).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditCompanyCommandValidator : AbstractValidator<EditCompanyCommand>
{
    readonly ApplicationContext _context;

    public EditCompanyCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CompanyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Company with id {PropertyValue} does not exists");
        RuleFor(x => x.CompanyCode).MustAsync(async (request, companyCode, cancellation) => await _context.NotExists<CompanyState>(x => x.CompanyCode == companyCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Company with companyCode {PropertyValue} already exists");
	RuleFor(x => x.CompanyName).MustAsync(async (request, companyName, cancellation) => await _context.NotExists<CompanyState>(x => x.CompanyName == companyName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Company with companyName {PropertyValue} already exists");
	
    }
}
