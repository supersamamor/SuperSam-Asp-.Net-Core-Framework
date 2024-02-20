using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.DSF.Application.Features.DSF.TaskApprover.Commands;

public record DeleteTaskApproverCommand : BaseCommand, IRequest<Validation<Error, TaskApproverState>>;

public class DeleteTaskApproverCommandHandler : BaseCommandHandler<ApplicationContext, TaskApproverState, DeleteTaskApproverCommand>, IRequestHandler<DeleteTaskApproverCommand, Validation<Error, TaskApproverState>>
{
    public DeleteTaskApproverCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTaskApproverCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskApproverState>> Handle(DeleteTaskApproverCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTaskApproverCommandValidator : AbstractValidator<DeleteTaskApproverCommand>
{
    readonly ApplicationContext _context;

    public DeleteTaskApproverCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskApproverState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskApprover with id {PropertyValue} does not exists");
    }
}
