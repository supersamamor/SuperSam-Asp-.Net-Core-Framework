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

public record EditProductCommand : ProductState, IRequest<Validation<Error, ProductState>>;

public class EditProductCommandHandler : BaseCommandHandler<ApplicationContext, ProductState, EditProductCommand>, IRequestHandler<EditProductCommand, Validation<Error, ProductState>>
{
    public EditProductCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProductCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProductState>> Handle(EditProductCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditProduct(request, cancellationToken));


	public async Task<Validation<Error, ProductState>> EditProduct(EditProductCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Product.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateProductImageList(entity, request, cancellationToken);		
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProductState>(entity);
	}
	
	private async Task UpdateProductImageList(ProductState entity, EditProductCommand request, CancellationToken cancellationToken)
	{
		IList<ProductImageState> productImageListForDeletion = new List<ProductImageState>();
		var queryProductImageForDeletion = Context.ProductImage.Where(l => l.ProductId == request.Id).AsNoTracking();
		if (entity.ProductImageList?.Count > 0)
		{
			queryProductImageForDeletion = queryProductImageForDeletion.Where(l => !(entity.ProductImageList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		productImageListForDeletion = await queryProductImageForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var productImage in productImageListForDeletion!)
		{
			Context.Entry(productImage).State = EntityState.Deleted;
		}
		if (entity.ProductImageList?.Count > 0)
		{
			foreach (var productImage in entity.ProductImageList.Where(l => !productImageListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ProductImageState>(x => x.Id == productImage.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(productImage).State = EntityState.Added;
				}
				else
				{
					Context.Entry(productImage).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    readonly ApplicationContext _context;

    public EditProductCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProductState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Product with id {PropertyValue} does not exists");
        RuleFor(x => x.BarcodeNumber).MustAsync(async (request, barcodeNumber, cancellation) => await _context.NotExists<ProductState>(x => x.BarcodeNumber == barcodeNumber && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Product with barcodeNumber {PropertyValue} already exists");
	
    }
}
