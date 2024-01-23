using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;

public record AddHealthDeclarationCommand : HealthDeclarationState, IRequest<Validation<Error, HealthDeclarationState>>;

public class AddHealthDeclarationCommandHandler : BaseCommandHandler<ApplicationContext, HealthDeclarationState, AddHealthDeclarationCommand>, IRequestHandler<AddHealthDeclarationCommand, Validation<Error, HealthDeclarationState>>
{
	private readonly IdentityContext _identityContext;
    public AddHealthDeclarationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddHealthDeclarationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, HealthDeclarationState>> Handle(AddHealthDeclarationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddHealthDeclarationCommandValidator : AbstractValidator<AddHealthDeclarationCommand>
{
    readonly ApplicationContext _context;

    public AddHealthDeclarationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<HealthDeclarationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("HealthDeclaration with id {PropertyValue} already exists");
        
    }
}
