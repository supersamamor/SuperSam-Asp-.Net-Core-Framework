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

public record EditSalesCategoryCommand : SalesCategoryState, IRequest<Validation<Error, SalesCategoryState>>;

public class EditSalesCategoryCommandHandler : BaseCommandHandler<ApplicationContext, SalesCategoryState, EditSalesCategoryCommand>, IRequestHandler<EditSalesCategoryCommand, Validation<Error, SalesCategoryState>>
{
    public EditSalesCategoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSalesCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SalesCategoryState>> Handle(EditSalesCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSalesCategoryCommandValidator : AbstractValidator<EditSalesCategoryCommand>
{
    readonly ApplicationContext _context;

    public EditSalesCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SalesCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SalesCategory with id {PropertyValue} does not exists");
        
    }
}
