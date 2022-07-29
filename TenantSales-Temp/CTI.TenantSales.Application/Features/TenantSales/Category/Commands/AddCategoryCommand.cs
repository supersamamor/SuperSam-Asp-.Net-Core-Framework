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

public record AddCategoryCommand : CategoryState, IRequest<Validation<Error, CategoryState>>;

public class AddCategoryCommandHandler : BaseCommandHandler<ApplicationContext, CategoryState, AddCategoryCommand>, IRequestHandler<AddCategoryCommand, Validation<Error, CategoryState>>
{
    public AddCategoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCategoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CategoryState>> Handle(AddCategoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    readonly ApplicationContext _context;

    public AddCategoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CategoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Category with id {PropertyValue} already exists");
        
    }
}
