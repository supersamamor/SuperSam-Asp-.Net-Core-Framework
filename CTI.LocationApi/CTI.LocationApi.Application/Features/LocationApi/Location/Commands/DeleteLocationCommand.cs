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

namespace CTI.LocationApi.Application.Features.LocationApi.Location.Commands;

public record DeleteLocationCommand : BaseCommand, IRequest<Validation<Error, LocationState>>;

public class DeleteLocationCommandHandler : BaseCommandHandler<ApplicationContext, LocationState, DeleteLocationCommand>, IRequestHandler<DeleteLocationCommand, Validation<Error, LocationState>>
{
    public DeleteLocationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteLocationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LocationState>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{
    readonly ApplicationContext _context;

    public DeleteLocationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LocationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Location with id {PropertyValue} does not exists");
    }
}
