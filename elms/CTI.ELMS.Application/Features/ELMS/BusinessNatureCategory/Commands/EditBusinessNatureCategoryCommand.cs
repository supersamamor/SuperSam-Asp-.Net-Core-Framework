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

public record EditBusinessNatureCategoryCommand : BusinessNatureCategoryState, IRequest<Validation<Error, BusinessNatureCategoryState>>;

public class EditBusinessNatureCategoryCommandHandler : BaseCommandHandler<ApplicationContext, BusinessNatureCategoryState, EditBusinessNatureCategoryCommand>, IRequestHandler<EditBusinessNatureCategoryCommand, Validation<Error, BusinessNatureCategoryState>>
{
    public EditBusinessNatureCategoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditBusinessNatureCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, BusinessNatureCategoryState>> Handle(EditBusinessNatureCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditBusinessNatureCategoryCommandValidator : AbstractValidator<EditBusinessNatureCategoryCommand>
{
    readonly ApplicationContext _context;

    public EditBusinessNatureCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessNatureCategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessNatureCategory with id {PropertyValue} does not exists");
        
    }
}
