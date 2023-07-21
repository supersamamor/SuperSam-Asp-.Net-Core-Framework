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

public record EditPLHealthDeclarationCommand : PLHealthDeclarationState, IRequest<Validation<Error, PLHealthDeclarationState>>;

public class EditPLHealthDeclarationCommandHandler : BaseCommandHandler<ApplicationContext, PLHealthDeclarationState, EditPLHealthDeclarationCommand>, IRequestHandler<EditPLHealthDeclarationCommand, Validation<Error, PLHealthDeclarationState>>
{
    public EditPLHealthDeclarationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPLHealthDeclarationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, PLHealthDeclarationState>> Handle(EditPLHealthDeclarationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditPLHealthDeclarationCommandValidator : AbstractValidator<EditPLHealthDeclarationCommand>
{
    readonly ApplicationContext _context;

    public EditPLHealthDeclarationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PLHealthDeclarationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLHealthDeclaration with id {PropertyValue} does not exists");
        
    }
}
