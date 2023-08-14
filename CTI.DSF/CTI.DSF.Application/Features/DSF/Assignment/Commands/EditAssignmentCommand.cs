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

public record EditAssignmentCommand : AssignmentState, IRequest<Validation<Error, AssignmentState>>;

public class EditAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, AssignmentState, EditAssignmentCommand>, IRequestHandler<EditAssignmentCommand, Validation<Error, AssignmentState>>
{
    public EditAssignmentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditAssignmentCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, AssignmentState>> Handle(EditAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditAssignmentCommandValidator : AbstractValidator<EditAssignmentCommand>
{
    readonly ApplicationContext _context;

    public EditAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<AssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Assignment with id {PropertyValue} does not exists");
        RuleFor(x => x.AssignmentCode).MustAsync(async (request, assignmentCode, cancellation) => await _context.NotExists<AssignmentState>(x => x.AssignmentCode == assignmentCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Assignment with assignmentCode {PropertyValue} already exists");
	
    }
}
