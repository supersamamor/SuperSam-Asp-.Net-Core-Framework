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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.TaskTag.Commands;

public record EditTaskTagCommand : TaskTagState, IRequest<Validation<Error, TaskTagState>>;

public class EditTaskTagCommandHandler : BaseCommandHandler<ApplicationContext, TaskTagState, EditTaskTagCommand>, IRequestHandler<EditTaskTagCommand, Validation<Error, TaskTagState>>
{
    public EditTaskTagCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTaskTagCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TaskTagState>> Handle(EditTaskTagCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTaskTagCommandValidator : AbstractValidator<EditTaskTagCommand>
{
    readonly ApplicationContext _context;

    public EditTaskTagCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskTagState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskTag with id {PropertyValue} does not exists");
        
    }
}
