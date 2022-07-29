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

public record AddLevelCommand : LevelState, IRequest<Validation<Error, LevelState>>;

public class AddLevelCommandHandler : BaseCommandHandler<ApplicationContext, LevelState, AddLevelCommand>, IRequestHandler<AddLevelCommand, Validation<Error, LevelState>>
{
    public AddLevelCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLevelCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, LevelState>> Handle(AddLevelCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddLevelCommandValidator : AbstractValidator<AddLevelCommand>
{
    readonly ApplicationContext _context;

    public AddLevelCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LevelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Level with id {PropertyValue} already exists");
        
    }
}
