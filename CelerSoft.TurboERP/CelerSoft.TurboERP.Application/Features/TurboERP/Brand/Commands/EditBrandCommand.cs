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

public record EditBrandCommand : BrandState, IRequest<Validation<Error, BrandState>>;

public class EditBrandCommandHandler : BaseCommandHandler<ApplicationContext, BrandState, EditBrandCommand>, IRequestHandler<EditBrandCommand, Validation<Error, BrandState>>
{
    public EditBrandCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBrandCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, BrandState>> Handle(EditBrandCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditBrandCommandValidator : AbstractValidator<EditBrandCommand>
{
    readonly ApplicationContext _context;

    public EditBrandCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BrandState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Brand with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<BrandState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Brand with name {PropertyValue} already exists");
	
    }
}
