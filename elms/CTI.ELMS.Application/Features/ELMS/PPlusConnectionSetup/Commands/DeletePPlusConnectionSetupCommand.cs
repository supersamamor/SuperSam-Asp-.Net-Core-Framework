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

namespace CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Commands;

public record DeletePPlusConnectionSetupCommand : BaseCommand, IRequest<Validation<Error, PPlusConnectionSetupState>>;

public class DeletePPlusConnectionSetupCommandHandler : BaseCommandHandler<ApplicationContext, PPlusConnectionSetupState, DeletePPlusConnectionSetupCommand>, IRequestHandler<DeletePPlusConnectionSetupCommand, Validation<Error, PPlusConnectionSetupState>>
{
    public DeletePPlusConnectionSetupCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePPlusConnectionSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PPlusConnectionSetupState>> Handle(DeletePPlusConnectionSetupCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePPlusConnectionSetupCommandValidator : AbstractValidator<DeletePPlusConnectionSetupCommand>
{
    readonly ApplicationContext _context;

    public DeletePPlusConnectionSetupCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PPlusConnectionSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PPlusConnectionSetup with id {PropertyValue} does not exists");
    }
}
