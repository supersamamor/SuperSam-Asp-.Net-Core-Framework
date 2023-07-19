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

namespace CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Commands;

public record AddPLHealthDeclarationCommand : PLHealthDeclarationState, IRequest<Validation<Error, PLHealthDeclarationState>>;

public class AddPLHealthDeclarationCommandHandler : BaseCommandHandler<ApplicationContext, PLHealthDeclarationState, AddPLHealthDeclarationCommand>, IRequestHandler<AddPLHealthDeclarationCommand, Validation<Error, PLHealthDeclarationState>>
{
	private readonly IdentityContext _identityContext;
    public AddPLHealthDeclarationCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPLHealthDeclarationCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PLHealthDeclarationState>> Handle(AddPLHealthDeclarationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPLHealthDeclarationCommandValidator : AbstractValidator<AddPLHealthDeclarationCommand>
{
    readonly ApplicationContext _context;

    public AddPLHealthDeclarationCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PLHealthDeclarationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLHealthDeclaration with id {PropertyValue} already exists");
        
    }
}
