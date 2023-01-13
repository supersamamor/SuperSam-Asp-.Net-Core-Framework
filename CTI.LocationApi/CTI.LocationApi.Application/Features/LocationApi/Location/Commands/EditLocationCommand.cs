using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.LocationApi.Application.Features.LocationApi.Location.Commands;

public record EditLocationCommand : LocationState, IRequest<Validation<Error, LocationState>>;

public class EditLocationCommandHandler : BaseCommandHandler<ApplicationContext, LocationState, EditLocationCommand>, IRequestHandler<EditLocationCommand, Validation<Error, LocationState>>
{
    public EditLocationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLocationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, LocationState>> Handle(EditLocationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditLocationCommandValidator : AbstractValidator<EditLocationCommand>
{
    readonly ApplicationContext _context;

    public EditLocationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Location with id {PropertyValue} does not exists");
        
    }
}
