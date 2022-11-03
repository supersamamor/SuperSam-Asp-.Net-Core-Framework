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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Commands;

public record EditUserProjectAssignmentCommand : UserProjectAssignmentState, IRequest<Validation<Error, UserProjectAssignmentState>>;

public class EditUserProjectAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, UserProjectAssignmentState, EditUserProjectAssignmentCommand>, IRequestHandler<EditUserProjectAssignmentCommand, Validation<Error, UserProjectAssignmentState>>
{
    public EditUserProjectAssignmentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUserProjectAssignmentCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, UserProjectAssignmentState>> Handle(EditUserProjectAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditUserProjectAssignmentCommandValidator : AbstractValidator<EditUserProjectAssignmentCommand>
{
    readonly ApplicationContext _context;

    public EditUserProjectAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UserProjectAssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UserProjectAssignment with id {PropertyValue} does not exists");
        
    }
}
