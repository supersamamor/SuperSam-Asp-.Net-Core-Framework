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

namespace CTI.LocationApi.Application.Features.LocationApi.City.Commands;

public record DeleteCityCommand : BaseCommand, IRequest<Validation<Error, CityState>>;

public class DeleteCityCommandHandler : BaseCommandHandler<ApplicationContext, CityState, DeleteCityCommand>, IRequestHandler<DeleteCityCommand, Validation<Error, CityState>>
{
    public DeleteCityCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCityCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CityState>> Handle(DeleteCityCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
{
    readonly ApplicationContext _context;

    public DeleteCityCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("City with id {PropertyValue} does not exists");
    }
}
