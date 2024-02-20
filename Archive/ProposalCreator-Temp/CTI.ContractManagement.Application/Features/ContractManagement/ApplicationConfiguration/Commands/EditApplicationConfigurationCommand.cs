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

public record EditApplicationConfigurationCommand : ApplicationConfigurationState, IRequest<Validation<Error, ApplicationConfigurationState>>;

public class EditApplicationConfigurationCommandHandler : BaseCommandHandler<ApplicationContext, ApplicationConfigurationState, EditApplicationConfigurationCommand>, IRequestHandler<EditApplicationConfigurationCommand, Validation<Error, ApplicationConfigurationState>>
{
    public EditApplicationConfigurationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditApplicationConfigurationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ApplicationConfigurationState>> Handle(EditApplicationConfigurationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditApplicationConfigurationCommandValidator : AbstractValidator<EditApplicationConfigurationCommand>
{
    readonly ApplicationContext _context;

    public EditApplicationConfigurationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ApplicationConfigurationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ApplicationConfiguration with id {PropertyValue} does not exists");
        
    }
}
