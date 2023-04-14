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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Brand.Commands;

public record AddBrandCommand : BrandState, IRequest<Validation<Error, BrandState>>;

public class AddBrandCommandHandler : BaseCommandHandler<ApplicationContext, BrandState, AddBrandCommand>, IRequestHandler<AddBrandCommand, Validation<Error, BrandState>>
{
	private readonly IdentityContext _identityContext;
    public AddBrandCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBrandCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, BrandState>> Handle(AddBrandCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddBrandCommandValidator : AbstractValidator<AddBrandCommand>
{
    readonly ApplicationContext _context;

    public AddBrandCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BrandState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Brand with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<BrandState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Brand with name {PropertyValue} already exists");
	
    }
}
