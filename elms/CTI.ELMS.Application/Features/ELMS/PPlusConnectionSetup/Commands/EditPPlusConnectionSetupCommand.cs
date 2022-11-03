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

namespace CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Commands;

public record EditPPlusConnectionSetupCommand : PPlusConnectionSetupState, IRequest<Validation<Error, PPlusConnectionSetupState>>;

public class EditPPlusConnectionSetupCommandHandler : BaseCommandHandler<ApplicationContext, PPlusConnectionSetupState, EditPPlusConnectionSetupCommand>, IRequestHandler<EditPPlusConnectionSetupCommand, Validation<Error, PPlusConnectionSetupState>>
{
    public EditPPlusConnectionSetupCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPPlusConnectionSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PPlusConnectionSetupState>> Handle(EditPPlusConnectionSetupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPPlusConnectionSetupCommandValidator : AbstractValidator<EditPPlusConnectionSetupCommand>
{
    readonly ApplicationContext _context;

    public EditPPlusConnectionSetupCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PPlusConnectionSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PPlusConnectionSetup with id {PropertyValue} does not exists");
        RuleFor(x => x.PPlusVersionName).MustAsync(async (request, pPlusVersionName, cancellation) => await _context.NotExists<PPlusConnectionSetupState>(x => x.PPlusVersionName == pPlusVersionName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("PPlusConnectionSetup with pPlusVersionName {PropertyValue} already exists");
	
    }
}
