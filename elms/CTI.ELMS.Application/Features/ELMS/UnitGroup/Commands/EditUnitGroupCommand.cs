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

namespace CTI.ELMS.Application.Features.ELMS.UnitGroup.Commands;

public record EditUnitGroupCommand : UnitGroupState, IRequest<Validation<Error, UnitGroupState>>;

public class EditUnitGroupCommandHandler : BaseCommandHandler<ApplicationContext, UnitGroupState, EditUnitGroupCommand>, IRequestHandler<EditUnitGroupCommand, Validation<Error, UnitGroupState>>
{
    public EditUnitGroupCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUnitGroupCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, UnitGroupState>> Handle(EditUnitGroupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditUnitGroupCommandValidator : AbstractValidator<EditUnitGroupCommand>
{
    readonly ApplicationContext _context;

    public EditUnitGroupCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitGroupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitGroup with id {PropertyValue} does not exists");
        
    }
}
