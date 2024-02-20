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

namespace CTI.DSF.Application.Features.DSF.TaskMaster.Commands;

public record DeleteTaskMasterCommand : BaseCommand, IRequest<Validation<Error, TaskMasterState>>;

public class DeleteTaskMasterCommandHandler : BaseCommandHandler<ApplicationContext, TaskMasterState, DeleteTaskMasterCommand>, IRequestHandler<DeleteTaskMasterCommand, Validation<Error, TaskMasterState>>
{
    public DeleteTaskMasterCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTaskMasterCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskMasterState>> Handle(DeleteTaskMasterCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTaskMasterCommandValidator : AbstractValidator<DeleteTaskMasterCommand>
{
    readonly ApplicationContext _context;

    public DeleteTaskMasterCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskMasterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskMaster with id {PropertyValue} does not exists");
    }
}
