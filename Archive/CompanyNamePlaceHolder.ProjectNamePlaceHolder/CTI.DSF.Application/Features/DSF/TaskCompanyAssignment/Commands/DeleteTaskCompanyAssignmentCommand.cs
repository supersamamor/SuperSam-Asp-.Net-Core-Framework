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

namespace CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Commands;

public record DeleteTaskCompanyAssignmentCommand : BaseCommand, IRequest<Validation<Error, TaskCompanyAssignmentState>>;

public class DeleteTaskCompanyAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, TaskCompanyAssignmentState, DeleteTaskCompanyAssignmentCommand>, IRequestHandler<DeleteTaskCompanyAssignmentCommand, Validation<Error, TaskCompanyAssignmentState>>
{
    public DeleteTaskCompanyAssignmentCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTaskCompanyAssignmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskCompanyAssignmentState>> Handle(DeleteTaskCompanyAssignmentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTaskCompanyAssignmentCommandValidator : AbstractValidator<DeleteTaskCompanyAssignmentCommand>
{
    readonly ApplicationContext _context;

    public DeleteTaskCompanyAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskCompanyAssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskCompanyAssignment with id {PropertyValue} does not exists");
    }
}
