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

namespace CTI.LocationApi.Application.Features.LocationApi.City.Commands;

public record EditCityCommand : CityState, IRequest<Validation<Error, CityState>>;

public class EditCityCommandHandler : BaseCommandHandler<ApplicationContext, CityState, EditCityCommand>, IRequestHandler<EditCityCommand, Validation<Error, CityState>>
{
    public EditCityCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCityCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CityState>> Handle(EditCityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCityCommandValidator : AbstractValidator<EditCityCommand>
{
    readonly ApplicationContext _context;

    public EditCityCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("City with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<CityState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("City with code {PropertyValue} already exists");
	
    }
}
