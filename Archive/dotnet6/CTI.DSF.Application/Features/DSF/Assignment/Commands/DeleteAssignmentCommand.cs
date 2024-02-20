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

namespace CTI.DSF.Application.Features.DSF.Assignment.Commands;

public record DeleteAssignmentCommand : BaseCommand, IRequest<Validation<Error, AssignmentState>>;

public class DeleteAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, AssignmentState, DeleteAssignmentCommand>, IRequestHandler<DeleteAssignmentCommand, Validation<Error, AssignmentState>>
{
    public DeleteAssignmentCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteAssignmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, AssignmentState>> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteAssignmentCommandValidator : AbstractValidator<DeleteAssignmentCommand>
{
    readonly ApplicationContext _context;

    public DeleteAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<AssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Assignment with id {PropertyValue} does not exists");
    }
}
