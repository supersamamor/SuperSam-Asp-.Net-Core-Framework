using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record DeleteOfferingCommand : BaseCommand, IRequest<Validation<Error, OfferingState>>;

public class DeleteOfferingCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, DeleteOfferingCommand>, IRequestHandler<DeleteOfferingCommand, Validation<Error, OfferingState>>
{
    public DeleteOfferingCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteOfferingCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OfferingState>> Handle(DeleteOfferingCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteOfferingCommandValidator : AbstractValidator<DeleteOfferingCommand>
{
    readonly ApplicationContext _context;

    public DeleteOfferingCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} does not exists");
    }
}
