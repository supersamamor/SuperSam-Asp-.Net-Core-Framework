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

public record AddTaskTagCommand : TaskTagState, IRequest<Validation<Error, TaskTagState>>;

public class AddTaskTagCommandHandler : BaseCommandHandler<ApplicationContext, TaskTagState, AddTaskTagCommand>, IRequestHandler<AddTaskTagCommand, Validation<Error, TaskTagState>>
{
	private readonly IdentityContext _identityContext;
    public AddTaskTagCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTaskTagCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, TaskTagState>> Handle(AddTaskTagCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddTaskTagCommandValidator : AbstractValidator<AddTaskTagCommand>
{
    readonly ApplicationContext _context;

    public AddTaskTagCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TaskTagState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskTag with id {PropertyValue} already exists");
        
    }
}
