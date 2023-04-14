using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Commands;

public record AddUnitCommand : UnitState, IRequest<Validation<Error, UnitState>>;

public class AddUnitCommandHandler : BaseCommandHandler<ApplicationContext, UnitState, AddUnitCommand>, IRequestHandler<AddUnitCommand, Validation<Error, UnitState>>
{
	private readonly IdentityContext _identityContext;
    public AddUnitCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUnitCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UnitState>> Handle(AddUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddUnitCommandValidator : AbstractValidator<AddUnitCommand>
{
    readonly ApplicationContext _context;

    public AddUnitCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Unit with id {PropertyValue} already exists");
        RuleFor(x => x.Abbreviations).MustAsync(async (abbreviations, cancellation) => await _context.NotExists<UnitState>(x => x.Abbreviations == abbreviations, cancellationToken: cancellation)).WithMessage("Unit with abbreviations {PropertyValue} already exists");
	RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<UnitState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Unit with name {PropertyValue} already exists");
	
    }
}
