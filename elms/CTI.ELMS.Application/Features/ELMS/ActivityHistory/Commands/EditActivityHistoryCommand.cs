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

namespace CTI.ELMS.Application.Features.ELMS.ActivityHistory.Commands;

public record EditActivityHistoryCommand : ActivityHistoryState, IRequest<Validation<Error, ActivityHistoryState>>;

public class EditActivityHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ActivityHistoryState, EditActivityHistoryCommand>, IRequestHandler<EditActivityHistoryCommand, Validation<Error, ActivityHistoryState>>
{
    public EditActivityHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditActivityHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ActivityHistoryState>> Handle(EditActivityHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditActivityHistory(request, cancellationToken));


	public async Task<Validation<Error, ActivityHistoryState>> EditActivityHistory(EditActivityHistoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ActivityHistory.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateUnitActivityList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ActivityHistoryState>(entity);
	}
	
	private async Task UpdateUnitActivityList(ActivityHistoryState entity, EditActivityHistoryCommand request, CancellationToken cancellationToken)
	{
		IList<UnitActivityState> unitActivityListForDeletion = new List<UnitActivityState>();
		var queryUnitActivityForDeletion = Context.UnitActivity.Where(l => l.ActivityHistoryID == request.Id).AsNoTracking();
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

public class EditActivityHistoryCommandValidator : AbstractValidator<EditActivityHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditActivityHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ActivityHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ActivityHistory with id {PropertyValue} does not exists");
        
    }
}
