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

public record AddIFCAUnitInformationCommand : IFCAUnitInformationState, IRequest<Validation<Error, IFCAUnitInformationState>>;

public class AddIFCAUnitInformationCommandHandler : BaseCommandHandler<ApplicationContext, IFCAUnitInformationState, AddIFCAUnitInformationCommand>, IRequestHandler<AddIFCAUnitInformationCommand, Validation<Error, IFCAUnitInformationState>>
{
	private readonly IdentityContext _identityContext;
    public AddIFCAUnitInformationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddIFCAUnitInformationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, IFCAUnitInformationState>> Handle(AddIFCAUnitInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddIFCAUnitInformationCommandValidator : AbstractValidator<AddIFCAUnitInformationCommand>
{
    readonly ApplicationContext _context;

    public AddIFCAUnitInformationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<IFCAUnitInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAUnitInformation with id {PropertyValue} already exists");
        
    }
}
