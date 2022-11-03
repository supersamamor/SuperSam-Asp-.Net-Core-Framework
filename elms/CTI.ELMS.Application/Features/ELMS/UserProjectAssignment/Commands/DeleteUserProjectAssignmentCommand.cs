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

namespace CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Commands;

public record DeleteUserProjectAssignmentCommand : BaseCommand, IRequest<Validation<Error, UserProjectAssignmentState>>;

public class DeleteUserProjectAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, UserProjectAssignmentState, DeleteUserProjectAssignmentCommand>, IRequestHandler<DeleteUserProjectAssignmentCommand, Validation<Error, UserProjectAssignmentState>>
{
    public DeleteUserProjectAssignmentCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUserProjectAssignmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UserProjectAssignmentState>> Handle(DeleteUserProjectAssignmentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUserProjectAssignmentCommandValidator : AbstractValidator<DeleteUserProjectAssignmentCommand>
{
    readonly ApplicationContext _context;

    public DeleteUserProjectAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UserProjectAssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UserProjectAssignment with id {PropertyValue} does not exists");
    }
}
