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

namespace CTI.ELMS.Application.Features.ELMS.Salutation.Commands;

public record DeleteSalutationCommand : BaseCommand, IRequest<Validation<Error, SalutationState>>;

public class DeleteSalutationCommandHandler : BaseCommandHandler<ApplicationContext, SalutationState, DeleteSalutationCommand>, IRequestHandler<DeleteSalutationCommand, Validation<Error, SalutationState>>
{
    public DeleteSalutationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSalutationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SalutationState>> Handle(DeleteSalutationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSalutationCommandValidator : AbstractValidator<DeleteSalutationCommand>
{
    readonly ApplicationContext _context;

    public DeleteSalutationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SalutationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Salutation with id {PropertyValue} does not exists");
    }
}
