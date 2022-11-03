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

namespace CTI.ELMS.Application.Features.ELMS.Activity.Commands;

public record EditActivityCommand : ActivityState, IRequest<Validation<Error, ActivityState>>;

public class EditActivityCommandHandler : BaseCommandHandler<ApplicationContext, ActivityState, EditActivityCommand>, IRequestHandler<EditActivityCommand, Validation<Error, ActivityState>>
{
    public EditActivityCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditActivityCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ActivityState>> Handle(EditActivityCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditActivity(request, cancellationToken));


	public async Task<Validation<Error, ActivityState>> EditActivity(EditActivityCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Activity.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateActivityHistoryList(entity, request, cancellationToken);
		await UpdateUnitActivityList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ActivityState>(entity);
	}
	
	private async Task UpdateActivityHistoryList(ActivityState entity, EditActivityCommand request, CancellationToken cancellationToken)
	{
		IList<ActivityHistoryState> activityHistoryListForDeletion = new List<ActivityHistoryState>();
		var queryActivityHistoryForDeletion = Context.ActivityHistory.Where(l => l.ActivityID == request.Id).AsNoTracking();
		if (entity.ActivityHistoryList?.Count > 0)
		{
			queryActivityHistoryForDeletion = queryActivityHistoryForDeletion.Where(l => !(entity.ActivityHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		activityHistoryListForDeletion = await queryActivityHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var activityHistory in activityHistoryListForDeletion!)
		{
			Context.Entry(activityHistory).State = EntityState.Deleted;
		}
		if (entity.ActivityHistoryList?.Count > 0)
		{
			foreach (var activityHistory in entity.ActivityHistoryList.Where(l => !activityHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ActivityHistoryState>(x => x.Id == activityHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(activityHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(activityHistory).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateUnitActivityList(ActivityState entity, EditActivityCommand request, CancellationToken cancellationToken)
	{
		IList<UnitActivityState> unitActivityListForDeletion = new List<UnitActivityState>();
		var queryUnitActivityForDeletion = Context.UnitActivity.Where(l => l.ActivityID == request.Id).AsNoTracking();
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
	
}

public class EditActivityCommandValidator : AbstractValidator<EditActivityCommand>
{
    readonly ApplicationContext _context;

    public EditActivityCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ActivityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Activity with id {PropertyValue} does not exists");
        
    }
}
