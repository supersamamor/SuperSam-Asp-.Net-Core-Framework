using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Product.Commands;

public record AddProductCommand : ProductState, IRequest<Validation<Error, ProductState>>;

public class AddProductCommandHandler : BaseCommandHandler<ApplicationContext, ProductState, AddProductCommand>, IRequestHandler<AddProductCommand, Validation<Error, ProductState>>
{
	private readonly IdentityContext _identityContext;
    public AddProductCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProductCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ProductState>> Handle(AddProductCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddProduct(request, cancellationToken));


	public async Task<Validation<Error, ProductState>> AddProduct(AddProductCommand request, CancellationToken cancellationToken)
	{
		ProductState entity = Mapper.Map<ProductState>(request);
		UpdateProductImageList(entity);		
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProductState>(entity);
	}
	
	private void UpdateProductImageList(ProductState entity)
	{
		if (entity.ProductImageList?.Count > 0)
		{
			foreach (var productImage in entity.ProductImageList!)
			{
				Context.Entry(productImage).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    readonly ApplicationContext _context;

    public AddProductCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProductState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Product with id {PropertyValue} already exists");
        RuleFor(x => x.BarcodeNumber).MustAsync(async (barcodeNumber, cancellation) => await _context.NotExists<ProductState>(x => x.BarcodeNumber == barcodeNumber, cancellationToken: cancellation)).WithMessage("Product with barcodeNumber {PropertyValue} already exists");
	
    }
}
