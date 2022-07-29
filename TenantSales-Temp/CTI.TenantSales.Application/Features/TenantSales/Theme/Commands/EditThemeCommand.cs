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

namespace CTI.TenantSales.Application.Features.TenantSales.Theme.Commands;

public record EditThemeCommand : ThemeState, IRequest<Validation<Error, ThemeState>>;

public class EditThemeCommandHandler : BaseCommandHandler<ApplicationContext, ThemeState, EditThemeCommand>, IRequestHandler<EditThemeCommand, Validation<Error, ThemeState>>
{
    public EditThemeCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditThemeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ThemeState>> Handle(EditThemeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditTheme(request, cancellationToken));


	public async Task<Validation<Error, ThemeState>> EditTheme(EditThemeCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Theme.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateClassificationList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ThemeState>(entity);
	}
	
	private async Task UpdateClassificationList(ThemeState entity, EditThemeCommand request, CancellationToken cancellationToken)
	{
		IList<ClassificationState> classificationListForDeletion = new List<ClassificationState>();
		var queryClassificationForDeletion = Context.Classification.Where(l => l.ThemeId == request.Id).AsNoTracking();
		if (entity.ClassificationList?.Count > 0)
		{
			queryClassificationForDeletion = queryClassificationForDeletion.Where(l => !(entity.ClassificationList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		classificationListForDeletion = await queryClassificationForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var classification in classificationListForDeletion!)
		{
			Context.Entry(classification).State = EntityState.Deleted;
		}
		if (entity.ClassificationList?.Count > 0)
		{
			foreach (var classification in entity.ClassificationList.Where(l => !classificationListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ClassificationState>(x => x.Id == classification.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(classification).State = EntityState.Added;
				}
				else
				{
					Context.Entry(classification).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditThemeCommandValidator : AbstractValidator<EditThemeCommand>
{
    readonly ApplicationContext _context;

    public EditThemeCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ThemeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Theme with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<ThemeState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Theme with name {PropertyValue} already exists");
	RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<ThemeState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Theme with code {PropertyValue} already exists");
	
    }
}
