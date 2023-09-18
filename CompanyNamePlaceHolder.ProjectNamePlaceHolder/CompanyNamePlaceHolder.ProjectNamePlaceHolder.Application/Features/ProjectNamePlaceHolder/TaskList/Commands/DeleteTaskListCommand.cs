using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.TaskList.Commands;

public record DeleteTaskListCommand : BaseCommand, IRequest<Validation<Error, TaskListState>>;

public class DeleteTaskListCommandHandler : BaseCommandHandler<ApplicationContext, TaskListState, DeleteTaskListCommand>, IRequestHandler<DeleteTaskListCommand, Validation<Error, TaskListState>>
{
    public DeleteTaskListCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTaskListCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskListState>> Handle(DeleteTaskListCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTaskListCommandValidator : AbstractValidator<DeleteTaskListCommand>
{
    readonly ApplicationContext _context;

    public DeleteTaskListCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskListState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskList with id {PropertyValue} does not exists");
    }
}
