using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;

public record AddTestCommand : TestState, IRequest<Validation<Error, TestState>>;

public class AddTestCommandHandler : BaseCommandHandler<ApplicationContext, TestState, AddTestCommand>, IRequestHandler<AddTestCommand, Validation<Error, TestState>>
{
	private readonly IdentityContext _identityContext;
    public AddTestCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTestCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, TestState>> Handle(AddTestCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddTestCommandValidator : AbstractValidator<AddTestCommand>
{
    readonly ApplicationContext _context;

    public AddTestCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TestState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Test with id {PropertyValue} already exists");
        
    }
}
