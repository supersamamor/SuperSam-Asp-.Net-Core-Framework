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

public record AddProductImageCommand : ProductImageState, IRequest<Validation<Error, ProductImageState>>;

public class AddProductImageCommandHandler : BaseCommandHandler<ApplicationContext, ProductImageState, AddProductImageCommand>, IRequestHandler<AddProductImageCommand, Validation<Error, ProductImageState>>
{
	private readonly IdentityContext _identityContext;
    public AddProductImageCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProductImageCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProductImageState>> Handle(AddProductImageCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
{
    readonly ApplicationContext _context;

    public AddProductImageCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProductImageState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProductImage with id {PropertyValue} already exists");
        
    }
}
