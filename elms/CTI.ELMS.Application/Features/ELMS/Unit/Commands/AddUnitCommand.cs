using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.Unit.Commands;

public record AddUnitCommand : UnitState, IRequest<Validation<Error, UnitState>>;

public class AddUnitCommandHandler : BaseCommandHandler<ApplicationContext, UnitState, AddUnitCommand>, IRequestHandler<AddUnitCommand, Validation<Error, UnitState>>
{
	private readonly IdentityContext _identityContext;
    public AddUnitCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUnitCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, UnitState>> Handle(AddUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddUnit(request, cancellationToken));


	public async Task<Validation<Error, UnitState>> AddUnit(AddUnitCommand request, CancellationToken cancellationToken)
	{
		UnitState entity = Mapper.Map<UnitState>(request);
		UpdateUnitActivityList(entity);
		UpdatePreSelectedUnitList(entity);
		UpdateUnitOfferedList(entity);
		UpdateUnitOfferedHistoryList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, UnitState>(entity);
	}
	
	private void UpdateUnitActivityList(UnitState entity)
	{
		if (entity.UnitActivityList?.Count > 0)
		{
			foreach (var unitActivity in entity.UnitActivityList!)
			{
				Context.Entry(unitActivity).State = EntityState.Added;
			}
		}
	}
	private void UpdatePreSelectedUnitList(UnitState entity)
	{
		if (entity.PreSelectedUnitList?.Count > 0)
		{
			foreach (var preSelectedUnit in entity.PreSelectedUnitList!)
			{
				Context.Entry(preSelectedUnit).State = EntityState.Added;
			}
		}
	}
	private void UpdateUnitOfferedList(UnitState entity)
	{
		if (entity.UnitOfferedList?.Count > 0)
		{
			foreach (var unitOffered in entity.UnitOfferedList!)
			{
				Context.Entry(unitOffered).State = EntityState.Added;
			}
		}
	}
	private void UpdateUnitOfferedHistoryList(UnitState entity)
	{
		if (entity.UnitOfferedHistoryList?.Count > 0)
		{
			foreach (var unitOfferedHistory in entity.UnitOfferedHistoryList!)
			{
				Context.Entry(unitOfferedHistory).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddUnitCommandValidator : AbstractValidator<AddUnitCommand>
{
    readonly ApplicationContext _context;

    public AddUnitCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Unit with id {PropertyValue} already exists");
        
    }
}
