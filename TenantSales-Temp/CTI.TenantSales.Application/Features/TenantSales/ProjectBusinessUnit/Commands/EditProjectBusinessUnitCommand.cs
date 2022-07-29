using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Commands;

public record EditProjectBusinessUnitCommand : ProjectBusinessUnitState, IRequest<Validation<Error, ProjectBusinessUnitState>>;

public class EditProjectBusinessUnitCommandHandler : BaseCommandHandler<ApplicationContext, ProjectBusinessUnitState, EditProjectBusinessUnitCommand>, IRequestHandler<EditProjectBusinessUnitCommand, Validation<Error, ProjectBusinessUnitState>>
{
    public EditProjectBusinessUnitCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectBusinessUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectBusinessUnitState>> Handle(EditProjectBusinessUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectBusinessUnitCommandValidator : AbstractValidator<EditProjectBusinessUnitCommand>
{
    readonly ApplicationContext _context;

    public EditProjectBusinessUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectBusinessUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectBusinessUnit with id {PropertyValue} does not exists");
        
    }
}
