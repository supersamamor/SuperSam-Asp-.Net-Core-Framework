using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Application.Repositories;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;

public record AddTenantPOSSalesCommand : TenantPOSSalesState, IRequest<Validation<Error, TenantPOSSalesState>>;

public class AddTenantPOSSalesCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSSalesState, AddTenantPOSSalesCommand>, IRequestHandler<AddTenantPOSSalesCommand, Validation<Error, TenantPOSSalesState>>
{
    private readonly decimal _taxRate;
    private readonly decimal _salesAmountThreshold;
    private readonly TenantPOSSalesRepository _tenantPOSSalesRepository;
    public AddTenantPOSSalesCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTenantPOSSalesCommand> validator, IConfiguration configuration,
                                    TenantPOSSalesRepository tenantPOSSalesRepository) : base(context, mapper, validator)
    {
        _taxRate = configuration.GetValue<decimal>("TaxRate");
        _salesAmountThreshold = configuration.GetValue<decimal>("SalesAmountThreshold");
        _tenantPOSSalesRepository = tenantPOSSalesRepository;
    }


    public async Task<Validation<Error, TenantPOSSalesState>> Handle(AddTenantPOSSalesCommand request, CancellationToken cancellationToken)
    {
        return await Validators.ValidateTAsync(request, cancellationToken).BindT(
                    async request => await AddTenantPOSSales(request, cancellationToken));
    }
    private async Task<Validation<Error, TenantPOSSalesState>> AddTenantPOSSales(AddTenantPOSSalesCommand request, CancellationToken cancellationToken = default)
    {
        var entity = Mapper.Map<TenantPOSSalesState>(request);
        //Get Previous Sales
        var previousSale = await _tenantPOSSalesRepository.GetPreviousDaySales(entity);
        //Validate Daily Sales      
        entity.ProcessPreviousDaySales(previousSale, _taxRate, _salesAmountThreshold);
        entity.SetEntity(await _tenantPOSSalesRepository.GetTenantPOSEntity(entity.TenantPOSId));
        Context.Add(entity);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, TenantPOSSalesState>(entity);
    }
   
}

public class AddTenantPOSSalesCommandValidator : AbstractValidator<AddTenantPOSSalesCommand>
{
    readonly ApplicationContext _context;

    public AddTenantPOSSalesCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TenantPOSSalesState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantPOSSales with id {PropertyValue} already exists");

    }
}
