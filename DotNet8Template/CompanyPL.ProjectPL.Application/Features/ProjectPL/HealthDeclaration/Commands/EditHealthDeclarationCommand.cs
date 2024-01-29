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

public record EditHealthDeclarationCommand : HealthDeclarationState, IRequest<Validation<Error, HealthDeclarationState>>;

public class EditHealthDeclarationCommandHandler : BaseCommandHandler<ApplicationContext, HealthDeclarationState, EditHealthDeclarationCommand>, IRequestHandler<EditHealthDeclarationCommand, Validation<Error, HealthDeclarationState>>
{
    public EditHealthDeclarationCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditHealthDeclarationCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, HealthDeclarationState>> Handle(EditHealthDeclarationCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditHealthDeclarationCommandValidator : AbstractValidator<EditHealthDeclarationCommand>
{
    readonly ApplicationContext _context;

    public EditHealthDeclarationCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<HealthDeclarationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("HealthDeclaration with id {PropertyValue} does not exists");
        
    }
}
