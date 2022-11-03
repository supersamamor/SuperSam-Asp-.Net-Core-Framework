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

namespace CTI.ELMS.Application.Features.ELMS.LeadTask.Commands;

public record EditLeadTaskCommand : LeadTaskState, IRequest<Validation<Error, LeadTaskState>>;

public class EditLeadTaskCommandHandler : BaseCommandHandler<ApplicationContext, LeadTaskState, EditLeadTaskCommand>, IRequestHandler<EditLeadTaskCommand, Validation<Error, LeadTaskState>>
{
    public EditLeadTaskCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditLeadTaskCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LeadTaskState>> Handle(EditLeadTaskCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditLeadTask(request, cancellationToken));


	public async Task<Validation<Error, LeadTaskState>> EditLeadTask(EditLeadTaskCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.LeadTask.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateActivityHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, LeadTaskState>(entity);
	}
	
	private async Task UpdateActivityHistoryList(LeadTaskState entity, EditLeadTaskCommand request, CancellationToken cancellationToken)
	{
		IList<ActivityHistoryState> activityHistoryListForDeletion = new List<ActivityHistoryState>();
		var queryActivityHistoryForDeletion = Context.ActivityHistory.Where(l => l.LeadTaskId == request.Id).AsNoTracking();
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
	
}

public class EditLeadTaskCommandValidator : AbstractValidator<EditLeadTaskCommand>
{
    readonly ApplicationContext _context;

    public EditLeadTaskCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LeadTaskState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("LeadTask with id {PropertyValue} does not exists");
        RuleFor(x => x.LeadTaskName).MustAsync(async (request, leadTaskName, cancellation) => await _context.NotExists<LeadTaskState>(x => x.LeadTaskName == leadTaskName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("LeadTask with leadTaskName {PropertyValue} already exists");
	
    }
}
