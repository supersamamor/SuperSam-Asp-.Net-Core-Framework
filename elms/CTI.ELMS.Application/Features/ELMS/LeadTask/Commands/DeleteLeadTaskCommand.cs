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

namespace CTI.ELMS.Application.Features.ELMS.LeadTask.Commands;

public record DeleteLeadTaskCommand : BaseCommand, IRequest<Validation<Error, LeadTaskState>>;

public class DeleteLeadTaskCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskState, DeleteLeadTaskCommand>, IRequestHandler<DeleteLeadTaskCommand, Validation<Error, LeadTaskState>>
{
    public DeleteLeadTaskCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteLeadTaskCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LeadTaskState>> Handle(DeleteLeadTaskCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLeadTaskCommandValidator : AbstractValidator<DeleteLeadTaskCommand>
{
    readonly ApplicationContext _context;

    public DeleteLeadTaskCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTaskState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTask with id {PropertyValue} does not exists");
    }
}
