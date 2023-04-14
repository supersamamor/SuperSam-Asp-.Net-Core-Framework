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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Commands;

public record EditProductImageCommand : ProductImageState, IRequest<Validation<Error, ProductImageState>>;

public class EditProductImageCommandHandler : BaseCommandHandler<ApplicationContext, ProductImageState, EditProductImageCommand>, IRequestHandler<EditProductImageCommand, Validation<Error, ProductImageState>>
{
    public EditProductImageCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProductImageCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProductImageState>> Handle(EditProductImageCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProductImageCommandValidator : AbstractValidator<EditProductImageCommand>
{
    readonly ApplicationContext _context;

    public EditProductImageCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProductImageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProductImage with id {PropertyValue} does not exists");
        
    }
}
