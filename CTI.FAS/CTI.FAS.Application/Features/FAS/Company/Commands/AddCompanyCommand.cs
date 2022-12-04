using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.Company.Commands;

public record AddCompanyCommand : CompanyState, IRequest<Validation<Error, CompanyState>>;

public class AddCompanyCommandHandler : BaseCommandHandler<ApplicationContext, CompanyState, AddCompanyCommand>, IRequestHandler<AddCompanyCommand, Validation<Error, CompanyState>>
{

    public AddCompanyCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCompanyCommand> validator) : base(context, mapper, validator)
    {
	
    }

    public async Task<Validation<Error, CompanyState>> Handle(AddCompanyCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddCompany(request, cancellationToken));


	public async Task<Validation<Error, CompanyState>> AddCompany(AddCompanyCommand request, CancellationToken cancellationToken)
	{
		CompanyState entity = Mapper.Map<CompanyState>(request);		
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CompanyState>(entity);
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
        
    }
}
