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

namespace CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Commands;

public record AddIFCATenantInformationCommand : IFCATenantInformationState, IRequest<Validation<Error, IFCATenantInformationState>>;

public class AddIFCATenantInformationCommandHandler : BaseCommandHandler<ApplicationContext, IFCATenantInformationState, AddIFCATenantInformationCommand>, IRequestHandler<AddIFCATenantInformationCommand, Validation<Error, IFCATenantInformationState>>
{
	private readonly IdentityContext _identityContext;
    public AddIFCATenantInformationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddIFCATenantInformationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, IFCATenantInformationState>> Handle(AddIFCATenantInformationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddIFCATenantInformationCommandValidator : AbstractValidator<AddIFCATenantInformationCommand>
{
    readonly ApplicationContext _context;

    public AddIFCATenantInformationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<IFCATenantInformationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCATenantInformation with id {PropertyValue} already exists");
        
    }
}
