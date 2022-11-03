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

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Commands;

public record DeleteLeadTaskNextStepCommand : BaseCommand, IRequest<Validation<Error, LeadTaskNextStepState>>;

public class DeleteLeadTaskNextStepCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskNextStepState, DeleteLeadTaskNextStepCommand>, IRequestHandler<DeleteLeadTaskNextStepCommand, Validation<Error, LeadTaskNextStepState>>
{
    public DeleteLeadTaskNextStepCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteLeadTaskNextStepCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LeadTaskNextStepState>> Handle(DeleteLeadTaskNextStepCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLeadTaskNextStepCommandValidator : AbstractValidator<DeleteLeadTaskNextStepCommand>
{
    readonly ApplicationContext _context;

    public DeleteLeadTaskNextStepCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTaskNextStepState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTaskNextStep with id {PropertyValue} does not exists");
    }
}
