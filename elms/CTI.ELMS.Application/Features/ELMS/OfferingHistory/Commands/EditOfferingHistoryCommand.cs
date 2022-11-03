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

namespace CTI.ELMS.Application.Features.ELMS.OfferingHistory.Commands;

public record EditOfferingHistoryCommand : OfferingHistoryState, IRequest<Validation<Error, OfferingHistoryState>>;

public class EditOfferingHistoryCommandHandler : BaseCommandHandler<ApplicationContext, OfferingHistoryState, EditOfferingHistoryCommand>, IRequestHandler<EditOfferingHistoryCommand, Validation<Error, OfferingHistoryState>>
{
    public EditOfferingHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditOfferingHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OfferingHistoryState>> Handle(EditOfferingHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditOfferingHistory(request, cancellationToken));


	public async Task<Validation<Error, OfferingHistoryState>> EditOfferingHistory(EditOfferingHistoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.OfferingHistory.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateUnitOfferedHistoryList(entity, request, cancellationToken);
		await UpdateUnitGroupList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OfferingHistoryState>(entity);
	}
	
	private async Task UpdateUnitOfferedHistoryList(OfferingHistoryState entity, EditOfferingHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<UnitOfferedHistoryState> unitOfferedHistoryListForDeletion = new List<UnitOfferedHistoryState>();
		var queryUnitOfferedHistoryForDeletion = Context.UnitOfferedHistory.Where(l => l.OfferingHistoryID == request.Id).AsNoTracking();
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
	private async Task UpdateUnitGroupList(OfferingHistoryState entity, EditOfferingHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<UnitGroupState> unitGroupListForDeletion = new List<UnitGroupState>();
		var queryUnitGroupForDeletion = Context.UnitGroup.Where(l => l.OfferingHistoryID == request.Id).AsNoTracking();
		if (entity.UnitGroupList?.Count > 0)
		{
			queryUnitGroupForDeletion = queryUnitGroupForDeletion.Where(l => !(entity.UnitGroupList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		unitGroupListForDeletion = await queryUnitGroupForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var unitGroup in unitGroupListForDeletion!)
		{
			Context.Entry(unitGroup).State = EntityState.Deleted;
		}
		if (entity.UnitGroupList?.Count > 0)
		{
			foreach (var unitGroup in entity.UnitGroupList.Where(l => !unitGroupListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<UnitGroupState>(x => x.Id == unitGroup.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(unitGroup).State = EntityState.Added;
				}
				else
				{
					Context.Entry(unitGroup).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditOfferingHistoryCommandValidator : AbstractValidator<EditOfferingHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditOfferingHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OfferingHistory with id {PropertyValue} does not exists");
        
    }
}
