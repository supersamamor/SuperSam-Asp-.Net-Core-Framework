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

namespace CTI.TenantSales.Application.Features.TenantSales.Classification.Commands;

public record AddClassificationCommand : ClassificationState, IRequest<Validation<Error, ClassificationState>>;

public class AddClassificationCommandHandler : BaseCommandHandler<ApplicationContext, ClassificationState, AddClassificationCommand>, IRequestHandler<AddClassificationCommand, Validation<Error, ClassificationState>>
{
    public AddClassificationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddClassificationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ClassificationState>> Handle(AddClassificationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddClassification(request, cancellationToken));


	public async Task<Validation<Error, ClassificationState>> AddClassification(AddClassificationCommand request, CancellationToken cancellationToken)
	{
		ClassificationState entity = Mapper.Map<ClassificationState>(request);
		UpdateCategoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ClassificationState>(entity);
	}
	
	private void UpdateCategoryList(ClassificationState entity)
	{
		if (entity.CategoryList?.Count > 0)
		{
			foreach (var category in entity.CategoryList!)
			{
				Context.Entry(category).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddClassificationCommandValidator : AbstractValidator<AddClassificationCommand>
{
    readonly ApplicationContext _context;

    public AddClassificationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ClassificationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Classification with id {PropertyValue} already exists");
        
    }
}
