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

public record AddThemeCommand : ThemeState, IRequest<Validation<Error, ThemeState>>;

public class AddThemeCommandHandler : BaseCommandHandler<ApplicationContext, ThemeState, AddThemeCommand>, IRequestHandler<AddThemeCommand, Validation<Error, ThemeState>>
{
    public AddThemeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddThemeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ThemeState>> Handle(AddThemeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddTheme(request, cancellationToken));


	public async Task<Validation<Error, ThemeState>> AddTheme(AddThemeCommand request, CancellationToken cancellationToken)
	{
		ThemeState entity = Mapper.Map<ThemeState>(request);
		UpdateClassificationList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ThemeState>(entity);
	}
	
	private void UpdateClassificationList(ThemeState entity)
	{
		if (entity.ClassificationList?.Count > 0)
		{
			foreach (var classification in entity.ClassificationList!)
			{
				Context.Entry(classification).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddThemeCommandValidator : AbstractValidator<AddThemeCommand>
{
    readonly ApplicationContext _context;

    public AddThemeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ThemeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Theme with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<ThemeState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Theme with name {PropertyValue} already exists");
	RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<ThemeState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Theme with code {PropertyValue} already exists");
	
    }
}
