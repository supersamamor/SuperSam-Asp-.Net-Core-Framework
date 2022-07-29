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

public record AddProjectBusinessUnitCommand : ProjectBusinessUnitState, IRequest<Validation<Error, ProjectBusinessUnitState>>;

public class AddProjectBusinessUnitCommandHandler : BaseCommandHandler<ApplicationContext, ProjectBusinessUnitState, AddProjectBusinessUnitCommand>, IRequestHandler<AddProjectBusinessUnitCommand, Validation<Error, ProjectBusinessUnitState>>
{
    public AddProjectBusinessUnitCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectBusinessUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectBusinessUnitState>> Handle(AddProjectBusinessUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectBusinessUnitCommandValidator : AbstractValidator<AddProjectBusinessUnitCommand>
{
    readonly ApplicationContext _context;

    public AddProjectBusinessUnitCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectBusinessUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectBusinessUnit with id {PropertyValue} already exists");
        
    }
}
