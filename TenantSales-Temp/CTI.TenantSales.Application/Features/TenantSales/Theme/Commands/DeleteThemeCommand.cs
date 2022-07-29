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

namespace CTI.TenantSales.Application.Features.TenantSales.Theme.Commands;

public record DeleteThemeCommand : BaseCommand, IRequest<Validation<Error, ThemeState>>;

public class DeleteThemeCommandHandler : BaseCommandHandler<ApplicationContext, ThemeState, DeleteThemeCommand>, IRequestHandler<DeleteThemeCommand, Validation<Error, ThemeState>>
{
    public DeleteThemeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteThemeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ThemeState>> Handle(DeleteThemeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteThemeCommandValidator : AbstractValidator<DeleteThemeCommand>
{
    readonly ApplicationContext _context;

    public DeleteThemeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ThemeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Theme with id {PropertyValue} does not exists");
    }
}
