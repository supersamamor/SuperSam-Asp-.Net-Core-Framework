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

namespace CTI.ELMS.Application.Features.ELMS.NextStep.Commands;

public record DeleteNextStepCommand : BaseCommand, IRequest<Validation<Error, NextStepState>>;

public class DeleteNextStepCommandHandler : BaseCommandHandler<ApplicationContext, NextStepState, DeleteNextStepCommand>, IRequestHandler<DeleteNextStepCommand, Validation<Error, NextStepState>>
{
    public DeleteNextStepCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteNextStepCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, NextStepState>> Handle(DeleteNextStepCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteNextStepCommandValidator : AbstractValidator<DeleteNextStepCommand>
{
    readonly ApplicationContext _context;

    public DeleteNextStepCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<NextStepState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("NextStep with id {PropertyValue} does not exists");
    }
}
