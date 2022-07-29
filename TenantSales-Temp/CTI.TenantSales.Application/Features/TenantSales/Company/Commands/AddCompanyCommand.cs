using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.Company.Commands;

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
			async request => await Add(request, cancellationToken));
	
	
	
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
