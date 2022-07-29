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

namespace CTI.TenantSales.Application.Features.TenantSales.Level.Commands;

public record EditLevelCommand : LevelState, IRequest<Validation<Error, LevelState>>;

public class EditLevelCommandHandler : BaseCommandHandler<ApplicationContext, LevelState, EditLevelCommand>, IRequestHandler<EditLevelCommand, Validation<Error, LevelState>>
{
    public EditLevelCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLevelCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, LevelState>> Handle(EditLevelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditLevelCommandValidator : AbstractValidator<EditLevelCommand>
{
    readonly ApplicationContext _context;

    public EditLevelCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LevelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Level with id {PropertyValue} does not exists");
        
    }
}
