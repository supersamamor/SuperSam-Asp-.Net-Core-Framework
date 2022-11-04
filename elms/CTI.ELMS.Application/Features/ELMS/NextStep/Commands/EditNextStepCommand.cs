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

namespace CTI.ELMS.Application.Features.ELMS.NextStep.Commands;

public record EditNextStepCommand : NextStepState, IRequest<Validation<Error, NextStepState>>;

public class EditNextStepCommandHandler : BaseCommandHandler<ApplicationContext, NextStepState, EditNextStepCommand>, IRequestHandler<EditNextStepCommand, Validation<Error, NextStepState>>
{
    public EditNextStepCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditNextStepCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, NextStepState>> Handle(EditNextStepCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditNextStep(request, cancellationToken));


	public async Task<Validation<Error, NextStepState>> EditNextStep(EditNextStepCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.NextStep.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateLeadTaskNextStepList(entity, request, cancellationToken);
		await UpdateActivityHistoryList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, NextStepState>(entity);
	}
	
	private async Task UpdateLeadTaskNextStepList(NextStepState entity, EditNextStepCommand request, CancellationToken cancellationToken)
	{
		IList<LeadTaskNextStepState> leadTaskNextStepListForDeletion = new List<LeadTaskNextStepState>();
		var queryLeadTaskNextStepForDeletion = Context.LeadTaskNextStep.Where(l => l.NextStepId == request.Id).AsNoTracking();
		if (entity.LeadTaskNextStepList?.Count > 0)
		{
			queryLeadTaskNextStepForDeletion = queryLeadTaskNextStepForDeletion.Where(l => !(entity.LeadTaskNextStepList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		leadTaskNextStepListForDeletion = await queryLeadTaskNextStepForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var leadTaskNextStep in leadTaskNextStepListForDeletion!)
		{
			Context.Entry(leadTaskNextStep).State = EntityState.Deleted;
		}
		if (entity.LeadTaskNextStepList?.Count > 0)
		{
			foreach (var leadTaskNextStep in entity.LeadTaskNextStepList.Where(l => !leadTaskNextStepListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<LeadTaskNextStepState>(x => x.Id == leadTaskNextStep.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(leadTaskNextStep).State = EntityState.Added;
				}
				else
				{
					Context.Entry(leadTaskNextStep).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateActivityHistoryList(NextStepState entity, EditNextStepCommand request, CancellationToken cancellationToken)
	{
		IList<ActivityHistoryState> activityHistoryListForDeletion = new List<ActivityHistoryState>();
		var queryActivityHistoryForDeletion = Context.ActivityHistory.Where(l => l.NextStepId == request.Id).AsNoTracking();
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

public class EditNextStepCommandValidator : AbstractValidator<EditNextStepCommand>
{
    readonly ApplicationContext _context;

    public EditNextStepCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<NextStepState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("NextStep with id {PropertyValue} does not exists");
        RuleFor(x => x.NextStepTaskName).MustAsync(async (request, nextStepTaskName, cancellation) => await _context.NotExists<NextStepState>(x => x.NextStepTaskName == nextStepTaskName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("NextStep with nextStepTaskName {PropertyValue} already exists");
	
    }
}
