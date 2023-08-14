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

public record AddCompanyCommand : CompanyState, IRequest<Validation<Error, CompanyState>>;

public class AddCompanyCommandHandler : BaseCommandHandler<ApplicationContext, CompanyState, AddCompanyCommand>, IRequestHandler<AddCompanyCommand, Validation<Error, CompanyState>>
{
	private readonly IdentityContext _identityContext;
    public AddCompanyCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCompanyCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, CompanyState>> Handle(AddCompanyCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddCompany(request, cancellationToken));


	public async Task<Validation<Error, CompanyState>> AddCompany(AddCompanyCommand request, CancellationToken cancellationToken)
	{
		CompanyState entity = Mapper.Map<CompanyState>(request);
		UpdateDepartmentList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CompanyState>(entity);
	}
	
	private void UpdateDepartmentList(CompanyState entity)
	{
		if (entity.DepartmentList?.Count > 0)
		{
			foreach (var department in entity.DepartmentList!)
			{
				Context.Entry(department).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddCompanyCommandValidator : AbstractValidator<AddCompanyCommand>
{
    readonly ApplicationContext _context;

    public AddCompanyCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CompanyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Company with id {PropertyValue} already exists");
        RuleFor(x => x.CompanyCode).MustAsync(async (companyCode, cancellation) => await _context.NotExists<CompanyState>(x => x.CompanyCode == companyCode, cancellationToken: cancellation)).WithMessage("Company with companyCode {PropertyValue} already exists");
	RuleFor(x => x.CompanyName).MustAsync(async (companyName, cancellation) => await _context.NotExists<CompanyState>(x => x.CompanyName == companyName, cancellationToken: cancellation)).WithMessage("Company with companyName {PropertyValue} already exists");
	
    }
}
