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

namespace CTI.DSF.Application.Features.DSF.TaskTag.Commands;

public record DeleteTaskTagCommand : BaseCommand, IRequest<Validation<Error, TaskTagState>>;

public class DeleteTaskTagCommandHandler : BaseCommandHandler<ApplicationContext, TaskTagState, DeleteTaskTagCommand>, IRequestHandler<DeleteTaskTagCommand, Validation<Error, TaskTagState>>
{
    public DeleteTaskTagCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTaskTagCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskTagState>> Handle(DeleteTaskTagCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTaskTagCommandValidator : AbstractValidator<DeleteTaskTagCommand>
{
    readonly ApplicationContext _context;

    public DeleteTaskTagCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskTagState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskTag with id {PropertyValue} does not exists");
    }
}
