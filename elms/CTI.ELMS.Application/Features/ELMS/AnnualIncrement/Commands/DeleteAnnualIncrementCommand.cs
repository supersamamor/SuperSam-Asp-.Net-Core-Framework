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

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrement.Commands;

public record DeleteAnnualIncrementCommand : BaseCommand, IRequest<Validation<Error, AnnualIncrementState>>;

public class DeleteAnnualIncrementCommandHandler : BaseCommandHandler<ApplicationContext, AnnualIncrementState, DeleteAnnualIncrementCommand>, IRequestHandler<DeleteAnnualIncrementCommand, Validation<Error, AnnualIncrementState>>
{
    public DeleteAnnualIncrementCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteAnnualIncrementCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, AnnualIncrementState>> Handle(DeleteAnnualIncrementCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteAnnualIncrementCommandValidator : AbstractValidator<DeleteAnnualIncrementCommand>
{
    readonly ApplicationContext _context;

    public DeleteAnnualIncrementCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<AnnualIncrementState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("AnnualIncrement with id {PropertyValue} does not exists");
    }
}
