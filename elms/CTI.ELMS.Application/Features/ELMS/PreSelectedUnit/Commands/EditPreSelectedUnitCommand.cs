using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Commands;

public record EditPreSelectedUnitCommand : PreSelectedUnitState, IRequest<Validation<Error, PreSelectedUnitState>>;

public class EditPreSelectedUnitCommandHandler : BaseCommandHandler<ApplicationContext, PreSelectedUnitState, EditPreSelectedUnitCommand>, IRequestHandler<EditPreSelectedUnitCommand, Validation<Error, PreSelectedUnitState>>
{
    public EditPreSelectedUnitCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPreSelectedUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PreSelectedUnitState>> Handle(EditPreSelectedUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPreSelectedUnitCommandValidator : AbstractValidator<EditPreSelectedUnitCommand>
{
    readonly ApplicationContext _context;

    public EditPreSelectedUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PreSelectedUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PreSelectedUnit with id {PropertyValue} does not exists");
        
    }
}
