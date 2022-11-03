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

public record AddUserProjectAssignmentCommand : UserProjectAssignmentState, IRequest<Validation<Error, UserProjectAssignmentState>>;

public class AddUserProjectAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, UserProjectAssignmentState, AddUserProjectAssignmentCommand>, IRequestHandler<AddUserProjectAssignmentCommand, Validation<Error, UserProjectAssignmentState>>
{
	private readonly IdentityContext _identityContext;
    public AddUserProjectAssignmentCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUserProjectAssignmentCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UserProjectAssignmentState>> Handle(AddUserProjectAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddUserProjectAssignmentCommandValidator : AbstractValidator<AddUserProjectAssignmentCommand>
{
    readonly ApplicationContext _context;

    public AddUserProjectAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UserProjectAssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UserProjectAssignment with id {PropertyValue} already exists");
        
    }
}
