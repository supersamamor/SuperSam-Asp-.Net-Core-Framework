using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Commands;

public record EditEnrolledPayeeEmailCommand : EnrolledPayeeEmailState, IRequest<Validation<Error, EnrolledPayeeEmailState>>;

public class EditEnrolledPayeeEmailCommandHandler : BaseCommandHandler<ApplicationContext, EnrolledPayeeEmailState, EditEnrolledPayeeEmailCommand>, IRequestHandler<EditEnrolledPayeeEmailCommand, Validation<Error, EnrolledPayeeEmailState>>
{
    public EditEnrolledPayeeEmailCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditEnrolledPayeeEmailCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, EnrolledPayeeEmailState>> Handle(EditEnrolledPayeeEmailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditEnrolledPayeeEmailCommandValidator : AbstractValidator<EditEnrolledPayeeEmailCommand>
{
    readonly ApplicationContext _context;

    public EditEnrolledPayeeEmailCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<EnrolledPayeeEmailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EnrolledPayeeEmail with id {PropertyValue} does not exists");
        
    }
}
