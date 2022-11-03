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

public record EditUnitCommand : UnitState, IRequest<Validation<Error, UnitState>>;

public class EditUnitCommandHandler : BaseCommandHandler<ApplicationContext, UnitState, EditUnitCommand>, IRequestHandler<EditUnitCommand, Validation<Error, UnitState>>
{
    public EditUnitCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitState>> Handle(EditUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditUnit(request, cancellationToken));


	public async Task<Validation<Error, UnitState>> EditUnit(EditUnitCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Unit.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateUnitActivityList(entity, request, cancellationToken);
		await UpdatePreSelectedUnitList(entity, request, cancellationToken);
		await UpdateUnitOfferedList(entity, request, cancellationToken);
		await UpdateUnitOfferedHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, UnitState>(entity);
	}
	
	private async Task UpdateUnitActivityList(UnitState entity, EditUnitCommand request, CancellationToken cancellationToken)
	{
		IList<UnitActivityState> unitActivityListForDeletion = new List<UnitActivityState>();
		var queryUnitActivityForDeletion = Context.UnitActivity.Where(l => l.UnitID == request.Id).AsNoTracking();
		if (entity.UnitActivityList?.Count > 0)
		{
			queryUnitActivityForDeletion = queryUnitActivityForDeletion.Where(l => !(entity.UnitActivityList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		unitActivityListForDeletion = await queryUnitActivityForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var unitActivity in unitActivityListForDeletion!)
		{
			Context.Entry(unitActivity).State = EntityState.Deleted;
		}
		if (entity.UnitActivityList?.Count > 0)
		{
			foreach (var unitActivity in entity.UnitActivityList.Where(l => !unitActivityListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<UnitActivityState>(x => x.Id == unitActivity.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(unitActivity).State = EntityState.Added;
				}
				else
				{
					Context.Entry(unitActivity).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdatePreSelectedUnitList(UnitState entity, EditUnitCommand request, CancellationToken cancellationToken)
	{
		IList<PreSelectedUnitState> preSelectedUnitListForDeletion = new List<PreSelectedUnitState>();
		var queryPreSelectedUnitForDeletion = Context.PreSelectedUnit.Where(l => l.UnitID == request.Id).AsNoTracking();
		if (entity.PreSelectedUnitList?.Count > 0)
		{
			queryPreSelectedUnitForDeletion = queryPreSelectedUnitForDeletion.Where(l => !(entity.PreSelectedUnitList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		preSelectedUnitListForDeletion = await queryPreSelectedUnitForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var preSelectedUnit in preSelectedUnitListForDeletion!)
		{
			Context.Entry(preSelectedUnit).State = EntityState.Deleted;
		}
		if (entity.PreSelectedUnitList?.Count > 0)
		{
			foreach (var preSelectedUnit in entity.PreSelectedUnitList.Where(l => !preSelectedUnitListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<PreSelectedUnitState>(x => x.Id == preSelectedUnit.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(preSelectedUnit).State = EntityState.Added;
				}
				else
				{
					Context.Entry(preSelectedUnit).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateUnitOfferedList(UnitState entity, EditUnitCommand request, CancellationToken cancellationToken)
	{
		IList<UnitOfferedState> unitOfferedListForDeletion = new List<UnitOfferedState>();
		var queryUnitOfferedForDeletion = Context.UnitOffered.Where(l => l.UnitID == request.Id).AsNoTracking();
		if (entity.UnitOfferedList?.Count > 0)
		{
			queryUnitOfferedForDeletion = queryUnitOfferedForDeletion.Where(l => !(entity.UnitOfferedList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		unitOfferedListForDeletion = await queryUnitOfferedForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var unitOffered in unitOfferedListForDeletion!)
		{
			Context.Entry(unitOffered).State = EntityState.Deleted;
		}
		if (entity.UnitOfferedList?.Count > 0)
		{
			foreach (var unitOffered in entity.UnitOfferedList.Where(l => !unitOfferedListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<UnitOfferedState>(x => x.Id == unitOffered.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(unitOffered).State = EntityState.Added;
				}
				else
				{
					Context.Entry(unitOffered).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateUnitOfferedHistoryList(UnitState entity, EditUnitCommand request, CancellationToken cancellationToken)
	{
		IList<UnitOfferedHistoryState> unitOfferedHistoryListForDeletion = new List<UnitOfferedHistoryState>();
		var queryUnitOfferedHistoryForDeletion = Context.UnitOfferedHistory.Where(l => l.UnitID == request.Id).AsNoTracking();
		if (entity.UnitOfferedHistoryList?.Count > 0)
		{
			queryUnitOfferedHistoryForDeletion = queryUnitOfferedHistoryForDeletion.Where(l => !(entity.UnitOfferedHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		unitOfferedHistoryListForDeletion = await queryUnitOfferedHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var unitOfferedHistory in unitOfferedHistoryListForDeletion!)
		{
			Context.Entry(unitOfferedHistory).State = EntityState.Deleted;
		}
		if (entity.UnitOfferedHistoryList?.Count > 0)
		{
			foreach (var unitOfferedHistory in entity.UnitOfferedHistoryList.Where(l => !unitOfferedHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<UnitOfferedHistoryState>(x => x.Id == unitOfferedHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(unitOfferedHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(unitOfferedHistory).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditUnitCommandValidator : AbstractValidator<EditUnitCommand>
{
    readonly ApplicationContext _context;

    public EditUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Unit with id {PropertyValue} does not exists");
        
    }
}
