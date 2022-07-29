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

namespace CTI.TenantSales.Application.Features.TenantSales.Category.Commands;

public record EditCategoryCommand : CategoryState, IRequest<Validation<Error, CategoryState>>;

public class EditCategoryCommandHandler : BaseCommandHandler<ApplicationContext, CategoryState, EditCategoryCommand>, IRequestHandler<EditCategoryCommand, Validation<Error, CategoryState>>
{
    public EditCategoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CategoryState>> Handle(EditCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    readonly ApplicationContext _context;

    public EditCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Category with id {PropertyValue} does not exists");
        
    }
}
