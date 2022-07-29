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

namespace CTI.TenantSales.Application.Features.TenantSales.Company.Commands;

public record DeleteCompanyCommand : BaseCommand, IRequest<Validation<Error, CompanyState>>;

public class DeleteCompanyCommandHandler : BaseCommandHandler<ApplicationContext, CompanyState, DeleteCompanyCommand>, IRequestHandler<DeleteCompanyCommand, Validation<Error, CompanyState>>
{
    public DeleteCompanyCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCompanyCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CompanyState>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    readonly ApplicationContext _context;

    public DeleteCompanyCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CompanyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Company with id {PropertyValue} does not exists");
    }
}
