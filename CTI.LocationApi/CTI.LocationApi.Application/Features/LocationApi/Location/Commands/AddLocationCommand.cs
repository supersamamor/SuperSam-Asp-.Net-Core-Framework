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

public record AddLocationCommand : LocationState, IRequest<Validation<Error, LocationState>>;

public class AddLocationCommandHandler : BaseCommandHandler<ApplicationContext, LocationState, AddLocationCommand>, IRequestHandler<AddLocationCommand, Validation<Error, LocationState>>
{

    public AddLocationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddLocationCommand> validator) : base(context, mapper, validator)
    {

    }

    
public async Task<Validation<Error, LocationState>> Handle(AddLocationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddLocationCommandValidator : AbstractValidator<AddLocationCommand>
{
    readonly ApplicationContext _context;

    public AddLocationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<LocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Location with id {PropertyValue} already exists");
        
    }
}
