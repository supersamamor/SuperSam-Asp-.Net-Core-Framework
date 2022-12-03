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

namespace CTI.FAS.Application.Features.FAS.Generated.Commands;

public record AddGeneratedCommand : GeneratedState, IRequest<Validation<Error, GeneratedState>>;

public class AddGeneratedCommandHandler : BaseCommandHandler<ApplicationContext, GeneratedState, AddGeneratedCommand>, IRequestHandler<AddGeneratedCommand, Validation<Error, GeneratedState>>
{
	private readonly IdentityContext _identityContext;
    public AddGeneratedCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddGeneratedCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, GeneratedState>> Handle(AddGeneratedCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddGeneratedCommandValidator : AbstractValidator<AddGeneratedCommand>
{
    readonly ApplicationContext _context;

    public AddGeneratedCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<GeneratedState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Generated with id {PropertyValue} already exists");
        
    }
}
