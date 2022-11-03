using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Commands;

public record AddBusinessNatureCategoryCommand : BusinessNatureCategoryState, IRequest<Validation<Error, BusinessNatureCategoryState>>;

public class AddBusinessNatureCategoryCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureCategoryState, AddBusinessNatureCategoryCommand>, IRequestHandler<AddBusinessNatureCategoryCommand, Validation<Error, BusinessNatureCategoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddBusinessNatureCategoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBusinessNatureCategoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, BusinessNatureCategoryState>> Handle(AddBusinessNatureCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddBusinessNatureCategoryCommandValidator : AbstractValidator<AddBusinessNatureCategoryCommand>
{
    readonly ApplicationContext _context;

    public AddBusinessNatureCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BusinessNatureCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNatureCategory with id {PropertyValue} already exists");
        
    }
}
