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

namespace CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Commands;

public record EditIFCAUnitInformationCommand : IFCAUnitInformationState, IRequest<Validation<Error, IFCAUnitInformationState>>;

public class EditIFCAUnitInformationCommandHandler : BaseCommandHandler<ApplicationContext, IFCAUnitInformationState, EditIFCAUnitInformationCommand>, IRequestHandler<EditIFCAUnitInformationCommand, Validation<Error, IFCAUnitInformationState>>
{
    public EditIFCAUnitInformationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditIFCAUnitInformationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, IFCAUnitInformationState>> Handle(EditIFCAUnitInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditIFCAUnitInformationCommandValidator : AbstractValidator<EditIFCAUnitInformationCommand>
{
    readonly ApplicationContext _context;

    public EditIFCAUnitInformationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCAUnitInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAUnitInformation with id {PropertyValue} does not exists");
        
    }
}
