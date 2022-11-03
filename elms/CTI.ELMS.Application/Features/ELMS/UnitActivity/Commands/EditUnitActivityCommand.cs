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

namespace CTI.ELMS.Application.Features.ELMS.UnitActivity.Commands;

public record EditUnitActivityCommand : UnitActivityState, IRequest<Validation<Error, UnitActivityState>>;

public class EditUnitActivityCommandHandler : BaseCommandHandler<ApplicationContext, UnitActivityState, EditUnitActivityCommand>, IRequestHandler<EditUnitActivityCommand, Validation<Error, UnitActivityState>>
{
    public EditUnitActivityCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUnitActivityCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, UnitActivityState>> Handle(EditUnitActivityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditUnitActivityCommandValidator : AbstractValidator<EditUnitActivityCommand>
{
    readonly ApplicationContext _context;

    public EditUnitActivityCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitActivityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitActivity with id {PropertyValue} does not exists");
        
    }
}
