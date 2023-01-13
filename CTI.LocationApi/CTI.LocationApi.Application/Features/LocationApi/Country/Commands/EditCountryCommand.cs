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

namespace CTI.LocationApi.Application.Features.LocationApi.Country.Commands;

public record EditCountryCommand : CountryState, IRequest<Validation<Error, CountryState>>;

public class EditCountryCommandHandler : BaseCommandHandler<ApplicationContext, CountryState, EditCountryCommand>, IRequestHandler<EditCountryCommand, Validation<Error, CountryState>>
{
    public EditCountryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditCountryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, CountryState>> Handle(EditCountryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditCountryCommandValidator : AbstractValidator<EditCountryCommand>
{
    readonly ApplicationContext _context;

    public EditCountryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CountryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Country with id {PropertyValue} does not exists");
        RuleFor(x => x.Name).MustAsync(async (request, name, cancellation) => await _context.NotExists<CountryState>(x => x.Name == name && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Country with name {PropertyValue} already exists");
	RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<CountryState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Country with code {PropertyValue} already exists");
	
    }
}
