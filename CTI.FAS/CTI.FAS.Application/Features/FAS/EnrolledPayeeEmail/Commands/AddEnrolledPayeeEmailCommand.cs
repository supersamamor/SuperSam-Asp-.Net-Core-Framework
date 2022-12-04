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

public record AddEnrolledPayeeEmailCommand : EnrolledPayeeEmailState, IRequest<Validation<Error, EnrolledPayeeEmailState>>;

public class AddEnrolledPayeeEmailCommandHandler : BaseCommandHandler<ApplicationContext, EnrolledPayeeEmailState, AddEnrolledPayeeEmailCommand>, IRequestHandler<AddEnrolledPayeeEmailCommand, Validation<Error, EnrolledPayeeEmailState>>
{
	private readonly IdentityContext _identityContext;
    public AddEnrolledPayeeEmailCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddEnrolledPayeeEmailCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, EnrolledPayeeEmailState>> Handle(AddEnrolledPayeeEmailCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddEnrolledPayeeEmailCommandValidator : AbstractValidator<AddEnrolledPayeeEmailCommand>
{
    readonly ApplicationContext _context;

    public AddEnrolledPayeeEmailCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<EnrolledPayeeEmailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("EnrolledPayeeEmail with id {PropertyValue} already exists");
        
    }
}
