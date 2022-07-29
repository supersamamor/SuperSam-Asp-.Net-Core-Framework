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

namespace CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Commands;

public record AddSalesCategoryCommand : SalesCategoryState, IRequest<Validation<Error, SalesCategoryState>>;

public class AddSalesCategoryCommandHandler : BaseCommandHandler<ApplicationContext, SalesCategoryState, AddSalesCategoryCommand>, IRequestHandler<AddSalesCategoryCommand, Validation<Error, SalesCategoryState>>
{
    public AddSalesCategoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSalesCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SalesCategoryState>> Handle(AddSalesCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddSalesCategoryCommandValidator : AbstractValidator<AddSalesCategoryCommand>
{
    readonly ApplicationContext _context;

    public AddSalesCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SalesCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SalesCategory with id {PropertyValue} already exists");
        
    }
}
