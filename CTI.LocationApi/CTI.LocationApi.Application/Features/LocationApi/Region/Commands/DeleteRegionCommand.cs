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

namespace CTI.LocationApi.Application.Features.LocationApi.Region.Commands;

public record DeleteRegionCommand : BaseCommand, IRequest<Validation<Error, RegionState>>;

public class DeleteRegionCommandHandler : BaseCommandHandler<ApplicationContext, RegionState, DeleteRegionCommand>, IRequestHandler<DeleteRegionCommand, Validation<Error, RegionState>>
{
    public DeleteRegionCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteRegionCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, RegionState>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteRegionCommandValidator : AbstractValidator<DeleteRegionCommand>
{
    readonly ApplicationContext _context;

    public DeleteRegionCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RegionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Region with id {PropertyValue} does not exists");
    }
}
