using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Commands;

public record AddApplicationConfigurationCommand : ApplicationConfigurationState, IRequest<Validation<Error, ApplicationConfigurationState>>;

public class AddApplicationConfigurationCommandHandler : BaseCommandHandler<ApplicationContext, ApplicationConfigurationState, AddApplicationConfigurationCommand>, IRequestHandler<AddApplicationConfigurationCommand, Validation<Error, ApplicationConfigurationState>>
{
	private readonly IdentityContext _identityContext;
    public AddApplicationConfigurationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddApplicationConfigurationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ApplicationConfigurationState>> Handle(AddApplicationConfigurationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddApplicationConfigurationCommandValidator : AbstractValidator<AddApplicationConfigurationCommand>
{
    readonly ApplicationContext _context;

    public AddApplicationConfigurationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ApplicationConfigurationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ApplicationConfiguration with id {PropertyValue} already exists");
        
    }
}
