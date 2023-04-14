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

public record EditUnitCommand : UnitState, IRequest<Validation<Error, UnitState>>;

public class EditUnitCommandHandler : BaseCommandHandler<ApplicationContext, UnitState, EditUnitCommand>, IRequestHandler<EditUnitCommand, Validation<Error, UnitState>>
{
    public EditUnitCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, UnitState>> Handle(EditUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditUnitCommandValidator : AbstractValidator<EditUnitCommand>
{
    readonly ApplicationContext _context;

    public EditUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Unit with id {PropertyValue} does not exists");
        RuleFor(x => x.Abbreviations).MustAsync(async (request, abbreviations, cancellation) => await _context.NotExists<UnitState>(x => x.Abbreviations == abbreviations && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Unit with abbreviations {PropertyValue} already exists");
	RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<UnitState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Unit with name {PropertyValue} already exists");
	
    }
}
