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

public record EditClassificationCommand : ClassificationState, IRequest<Validation<Error, ClassificationState>>;

public class EditClassificationCommandHandler : BaseCommandHandler<ApplicationContext, ClassificationState, EditClassificationCommand>, IRequestHandler<EditClassificationCommand, Validation<Error, ClassificationState>>
{
    public EditClassificationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditClassificationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ClassificationState>> Handle(EditClassificationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditClassification(request, cancellationToken));


	public async Task<Validation<Error, ClassificationState>> EditClassification(EditClassificationCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Classification.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateCategoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ClassificationState>(entity);
	}
	
	private async Task UpdateCategoryList(ClassificationState entity, EditClassificationCommand request, CancellationToken cancellationToken)
	{
		IList<CategoryState> categoryListForDeletion = new List<CategoryState>();
		var queryCategoryForDeletion = Context.Category.Where(l => l.ClassificationId == request.Id).AsNoTracking();
		if (entity.CategoryList?.Count > 0)
		{
			queryCategoryForDeletion = queryCategoryForDeletion.Where(l => !(entity.CategoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		categoryListForDeletion = await queryCategoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var category in categoryListForDeletion!)
		{
			Context.Entry(category).State = EntityState.Deleted;
		}
		if (entity.CategoryList?.Count > 0)
		{
			foreach (var category in entity.CategoryList.Where(l => !categoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<CategoryState>(x => x.Id == category.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(category).State = EntityState.Added;
				}
				else
				{
					Context.Entry(category).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditClassificationCommandValidator : AbstractValidator<EditClassificationCommand>
{
    readonly ApplicationContext _context;

    public EditClassificationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ClassificationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Classification with id {PropertyValue} does not exists");
        
    }
}
