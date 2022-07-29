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

namespace CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Commands;

public record AddBusinessUnitCommand : BusinessUnitState, IRequest<Validation<Error, BusinessUnitState>>;

public class AddBusinessUnitCommandHandler : BaseCommandHandler<ApplicationContext, BusinessUnitState, AddBusinessUnitCommand>, IRequestHandler<AddBusinessUnitCommand, Validation<Error, BusinessUnitState>>
{
    public AddBusinessUnitCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddBusinessUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BusinessUnitState>> Handle(AddBusinessUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddBusinessUnit(request, cancellationToken));


	public async Task<Validation<Error, BusinessUnitState>> AddBusinessUnit(AddBusinessUnitCommand request, CancellationToken cancellationToken)
	{
		BusinessUnitState entity = Mapper.Map<BusinessUnitState>(request);
		UpdateProjectBusinessUnitList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, BusinessUnitState>(entity);
	}
	
	private void UpdateProjectBusinessUnitList(BusinessUnitState entity)
	{
		if (entity.ProjectBusinessUnitList?.Count > 0)
		{
			foreach (var projectBusinessUnit in entity.ProjectBusinessUnitList!)
			{
				Context.Entry(projectBusinessUnit).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddBusinessUnitCommandValidator : AbstractValidator<AddBusinessUnitCommand>
{
    readonly ApplicationContext _context;

    public AddBusinessUnitCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<BusinessUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessUnit with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<BusinessUnitState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("BusinessUnit with name {PropertyValue} already exists");
	
    }
}
