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

namespace CTI.LocationApi.Application.Features.LocationApi.Country.Commands;

public record DeleteCountryCommand : BaseCommand, IRequest<Validation<Error, CountryState>>;

public class DeleteCountryCommandHandler : BaseCommandHandler<ApplicationContext, CountryState, DeleteCountryCommand>, IRequestHandler<DeleteCountryCommand, Validation<Error, CountryState>>
{
    public DeleteCountryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCountryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CountryState>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
{
    readonly ApplicationContext _context;

    public DeleteCountryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CountryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Country with id {PropertyValue} does not exists");
    }
}
