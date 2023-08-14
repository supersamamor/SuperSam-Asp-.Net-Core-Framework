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

namespace CTI.DSF.Application.Features.DSF.Assignment.Commands;

public record AddAssignmentCommand : AssignmentState, IRequest<Validation<Error, AssignmentState>>;

public class AddAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, AssignmentState, AddAssignmentCommand>, IRequestHandler<AddAssignmentCommand, Validation<Error, AssignmentState>>
{
	private readonly IdentityContext _identityContext;
    public AddAssignmentCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddAssignmentCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, AssignmentState>> Handle(AddAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddAssignmentCommandValidator : AbstractValidator<AddAssignmentCommand>
{
    readonly ApplicationContext _context;

    public AddAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<AssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Assignment with id {PropertyValue} already exists");
        RuleFor(x => x.AssignmentCode).MustAsync(async (assignmentCode, cancellation) => await _context.NotExists<AssignmentState>(x => x.AssignmentCode == assignmentCode, cancellationToken: cancellation)).WithMessage("Assignment with assignmentCode {PropertyValue} already exists");
	
    }
}
