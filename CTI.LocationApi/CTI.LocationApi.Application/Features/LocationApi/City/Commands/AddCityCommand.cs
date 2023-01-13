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

public record AddCityCommand : CityState, IRequest<Validation<Error, CityState>>;

public class AddCityCommandHandler : BaseCommandHandler<ApplicationContext, CityState, AddCityCommand>, IRequestHandler<AddCityCommand, Validation<Error, CityState>>
{
	
    public AddCityCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCityCommand> validator) : base(context, mapper, validator)
    {

    }

    
public async Task<Validation<Error, CityState>> Handle(AddCityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddCityCommandValidator : AbstractValidator<AddCityCommand>
{
    readonly ApplicationContext _context;

    public AddCityCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("City with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<CityState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("City with code {PropertyValue} already exists");
	
    }
}
